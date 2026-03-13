using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MineFiller))]
public class Field : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _expandSize = 3;

    private Dictionary<Vector2Int, Cell> _cells;
    private MineFiller _miner;

    public event Action CellClicked;
    public event Action Updated;

    private static readonly Vector2Int[] Directions =
    {
        new(-1, -1), new(0, -1), new(1, -1),
        new(-1, 0),              new(1, 0),
        new(-1, 1), new(0, 1),   new(1, 1)
    };

    private void Awake()
    {
        _miner = GetComponent<MineFiller>();
        _cells = new Dictionary<Vector2Int, Cell>();
    }


    public void HandleCellOpen(Cell cell)
    {
        cell.OnCellClicked -= HandleCellOpen;

        CellClicked?.Invoke();
        Updated?.Invoke();

        Vector2Int position = cell.Position;

        Expand(position, _expandSize);

    }

    public void Expand(Vector2Int startPosition, int size)
    {
        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector2Int position = startPosition + new Vector2Int(y, x);

                if (!_cells.ContainsKey(position))
                {
                    _cells.Add(position, InstantiateCell(position));
                }
                else
                {
                    if (_cells[position].NeighbourCount < 8)
                        UpdateNeighbours(_cells[position]);
                }
            }
        }
    }

    public IEnumerable<Cell> GetNeighbours(Vector2Int position)
    {
        foreach (var direction in Directions)
            if (_cells.TryGetValue(position + direction, out Cell neighbour))
                yield return neighbour;
    }

    public GameSaveData GetSaveData(int score)
    {
        GameSaveData save = new GameSaveData();
        save.Score = score;
        save.MineChance = _miner.MineChance;

        foreach (var cell in _cells.Values)
        {
            CellSaveData data = new CellSaveData();

            data.x = cell.Position.x;
            data.y = cell.Position.y;

            data.isMine = cell.IsMined;
            data.isOpened = cell.IsOpen;
            data.minesAround = cell.MinesAroundCount;

            save.CellDatas.Add(data);
        }

        return save;
    }

    public void LoadFromSave(GameSaveData save)
    {
        _cells.Clear();
        foreach (Transform child in transform)
            Destroy(child.gameObject);


        foreach (var data in save.CellDatas)
        {
            Vector2Int pos = new Vector2Int(data.x, data.y);

            Cell cell = Instantiate(
                _cellPrefab,
                new Vector3(pos.x, pos.y),
                Quaternion.identity,
                transform
            );

            cell.Initialize(pos, this);

            cell.SetMine(data.isMine);
            cell.SetMinesAroundCount(data.minesAround);

            cell.OnCellClicked += HandleCellOpen;

            if (data.isOpened)
                cell.Open();

                _cells.Add(pos, cell);
        }

        _miner.SetLoadChance(save.MineChance);

        Updated?.Invoke();
    }

    private Cell InstantiateCell(Vector2Int position)
    {
        Cell newCell = Instantiate(
               _cellPrefab,
               new Vector3(position.x, position.y, 0),
               Quaternion.identity,
               transform
           );


        newCell.Initialize(position, this);
        _miner.FillMines(newCell);
        UpdateNeighbours(newCell);

        newCell.OnCellClicked += HandleCellOpen;

        return newCell;
    }

    private void UpdateNeighbours(Cell cell)
    {
        UpdateCellValues(cell);
        UpdateNeighboursValues(cell.Position);
    }

    private void UpdateCellValues(Cell cell)
    {
        int neighboursCount = 0;
        int minesAround = 0;

        foreach (var direction in Directions)
        {
            if (_cells.TryGetValue(cell.Position + direction, out Cell neighbour))
            {
                neighboursCount++;
                if (neighbour.IsMined) minesAround++;
            }
        }

        cell.SetNeighboursCount(neighboursCount);
        cell.SetMinesAroundCount(minesAround);
    }

    private void UpdateNeighboursValues(Vector2Int position)
    {
        foreach (var direction in Directions)
            if (_cells.TryGetValue(position + direction, out Cell neighbour))
                UpdateCellValues(neighbour);
    }
}

