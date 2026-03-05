using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MineFiller))]
public class Field : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _expandSize = 3;

    private Dictionary<Vector2Int, Cell> _cells;

    private MineFiller _miner;

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


    public void HandleCellClick(Cell cell)
    {
        Vector2Int position = cell.Position;

        Expand(position, _expandSize);
    }

    public void Expand(Vector2Int startPosition, int size)
    {
        Dictionary<Vector2Int, Cell> newCells = new();

        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector2Int position = startPosition + new Vector2Int(x, y);

                if (!_cells.ContainsKey(position))
                {
                    Cell newCell = InstantiateCell(position);
                    _cells.Add(position, newCell);
                }
                else
                {
                    Cell checkedCell = _cells[position];
                    if (checkedCell.NeighbourCount < 8)
                        GetCellNeighbours(checkedCell);
                }
            }
        }
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

        GetCellNeighbours(newCell);

        _miner.FillMines(newCell);

        newCell.OnCellClicked += HandleCellClick;

        return newCell;
    }

    private void GetCellNeighbours(Cell cell)
    {
        int neighboursCount = 0;
        int minesAround = 0;

        foreach (var direction in Directions)
        {
            if (_cells.TryGetValue(cell.Position + direction, out Cell neighbour) && neighbour != null)
            {
                neighboursCount++;
                if (neighbour.IsMined) minesAround++;
            }
        }

        cell.SetNeighboursCount(neighboursCount);
        cell.SetMinesAroundCount(minesAround);
    }

    public IEnumerable<Cell> GetNeighbours(Vector2Int position)
    {
        foreach (var dir in Directions)
        {
            if (_cells.TryGetValue(position + dir, out Cell neighbour))
                yield return neighbour;
        }
    }
}

