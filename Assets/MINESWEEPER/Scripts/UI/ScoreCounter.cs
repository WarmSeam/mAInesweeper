using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Field _field;

    public int Score { get; private set; }

    public event Action<int> Changed;

    private void OnEnable()
    {
        _field.CellClicked += OnCellClicked;
    }

    private void OnDisable()
    {
        _field.CellClicked -= OnCellClicked;
    }

    public void SetStartValue(int startScore)
    {
        Score = startScore;
        Changed?.Invoke(Score);
    }

    private void OnCellClicked()
    {
        Score++;
        Changed?.Invoke(Score);
    }

}
