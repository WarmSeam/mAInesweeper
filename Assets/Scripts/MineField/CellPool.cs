using System.Collections.Generic;
using UnityEngine;

public class CellPool : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _initialSize = 200;

    private Queue<Cell> _pool = new Queue<Cell>();

    private void Awake()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            Cell cell = Instantiate(_cellPrefab, transform);
            cell.gameObject.SetActive(false);
            _pool.Enqueue(cell);
        }
    }

    public Cell Get()
    {
        if (_pool.Count > 0)
        {
            Cell cell = _pool.Dequeue();
            cell.gameObject.SetActive(true);
            return cell;
        }

        return Instantiate(_cellPrefab, transform);
    }

    public void Return(Cell cell)
    {
        cell.gameObject.SetActive(false);
        _pool.Enqueue(cell);
    }
}