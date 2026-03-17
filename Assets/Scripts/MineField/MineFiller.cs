using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineFiller : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _mineChance = 0.15f;
    [SerializeField] private float _assignCount = 150f;

    private float _currentChance = 0f;
    private float _currentAssignCount = 0f;

    public float MineChance => _mineChance;

    public event Action<float> ChanceChanged;

    private void Awake()
    {
        _currentAssignCount = _assignCount;
    }

    public void FillMines(Cell cell)
    {
        _currentAssignCount--;

        if (_currentAssignCount <= 0)
        {
            _mineChance += 0.01f;
            _currentAssignCount = _assignCount;
        }

        if (Random.value < _currentChance)
            cell.SetMine(true);

        _currentChance += 0.0005f;

        ChanceChanged?.Invoke(_mineChance);

        if (_currentChance > _mineChance)
            _currentChance = _mineChance;
    }

    public void SetLoadChance(float loadedChance)
    {
        _mineChance = loadedChance;

        ChanceChanged?.Invoke(_mineChance);
    }
}
