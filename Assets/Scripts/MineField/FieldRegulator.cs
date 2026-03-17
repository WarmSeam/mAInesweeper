using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MineFiller))]
public class FieldRegulator : MonoBehaviour
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

        Vector2Int position = cell.Position;

        Expand(position, _expandSize);

    }

    public void Expand(Vector2Int startPosition, int size)
    {
        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector2Int position = startPosition + new Vector2Int(x, y);

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

        Updated?.Invoke();
    }

    public IEnumerable<Cell> GetNeighbours(Vector2Int position)
    {
        foreach (var direction in Directions)
            if (_cells.TryGetValue(position + direction, out Cell neighbour))
                yield return neighbour;
    }

    public GameSaveData GetSaveData(int score)
    {
        GameSaveData save = new()
        {
            Score = score,
            MineChance = _miner.MineChance
        };

        foreach (var cell in _cells.Values)
        {
            CellSaveData data = cell.GetData();

            save.CellDatas.Add(data);
        }

        return save;
    }

    public void LoadFromSave(GameSaveData save)
    {
        _cells.Clear();

        Debug.Log(_cells.Count);

        foreach (Transform child in transform)
            Destroy(child.gameObject);


        foreach (var data in save.CellDatas)
        {
            Vector2Int position = new(data.x, data.y);

            Cell cell = Instantiate(
                _cellPrefab,
                new Vector3(position.x, position.y, 0),
                Quaternion.identity,
                transform
            );


            _cells.Add(position, cell);
            cell.SetData(data, this);

            cell.OnCellClicked += HandleCellOpen;

        }

        foreach (var cell in _cells.Values)
        {
            UpdateNeighbours(cell);
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

