using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MineFiller))]
public class FieldCreator : MonoBehaviour
{
    [SerializeField] private Field _field;

    private MineFiller _miner;

    private void Awake()
    {
        _miner = GetComponent<MineFiller>();
    }

    private void Start()
    {
        GenerateStartField(5);
    }

    private void GenerateStartField(int size)
    {
        _field.Expand(Vector2Int.zero, size);
        List<Cell> startCells = _field.InstantiateCells();
    }

    private void FillMines()
    {

    }
}
