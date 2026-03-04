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
{   new Vector2Int(-1, -1),new Vector2Int(0, -1),new Vector2Int(1, -1),
    new Vector2Int(-1, 0),                      new Vector2Int(1, 0),
    new Vector2Int(-1, 1),new Vector2Int(0, 1),new Vector2Int(1, 1)};

    private void Awake()
    {
        _miner = GetComponent<MineFiller>();
        _cells = new Dictionary<Vector2Int, Cell>();
    }

    public void AddCellData(Dictionary<Vector2Int, Cell> cells)
    {
        foreach (var item in cells)
        {
            if (!_cells.ContainsKey(item.Key))
                _cells.Add(item.Key, item.Value);
        }
    }

    public void Expand(Vector2Int startPosition, int size)
    {
        Dictionary<Vector2Int, Cell> newCells = new Dictionary<Vector2Int, Cell>();

        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector2Int position = startPosition + new Vector2Int(x, y);

                if (!_cells.ContainsKey(position))
                    newCells.Add(position, null);
            }
        }

        if (newCells.Count > 0)
        {
            AddCellData(newCells);
        }
    }

    public List<Cell> InstantiateCells()
    {
        List<Cell> newCells = new List<Cell>();

        foreach (var position in new List<Vector2Int>(_cells.Keys))
        {
            if (_cells[position] == null)
            {
                Cell newCell = Instantiate(_cellPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, transform);
                _cells[position] = newCell;

                newCell.OnCellClicked += HandleCellClick;

                newCells.Add(newCell);
            }
        }

        _miner.FillMines(newCells);

        SetupNeighbours();

        return newCells;
    }

    private void SetupNeighbours()
    {
        foreach (var pair in _cells)
        {
            Vector2Int position = pair.Key;
            Cell cell = pair.Value;

            List<Cell> neighbours = new List<Cell>();

            foreach (var dir in Directions)
            {
                Vector2Int neighbourPos = position + dir;

                if (_cells.TryGetValue(neighbourPos, out Cell neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }

            cell.SetNeighbours(neighbours);
        }
    }

    private void HandleCellClick(Cell cell)
    {
        Vector2Int cellPosition = new Vector2Int(Mathf.RoundToInt(cell.transform.position.x), Mathf.RoundToInt(cell.transform.position.y));
        Expand(cellPosition, _expandSize);
        InstantiateCells();
    }
}
