using System.Collections.Generic;
using UnityEngine;

public class MineFiller : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _mineChance = 0.15f;

    private float _currentChance = 0f;

    public void FillMines(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            if (Random.value < _currentChance)
                cell.PlaceMine();

            _currentChance += 0.0005f;

            if (_currentChance > _mineChance)
                _currentChance = _mineChance;
        }
    }
}
