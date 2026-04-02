using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Field _field;

    public int Score { get; private set; }

    public int Record {  get; private set; }

    public event Action<int> ScoreChanged;
    public event Action<int> RecordChanged;

    private void OnEnable()
    {
        _field.CellClicked += OnCellClicked;
    }

    private void OnDisable()
    {
        _field.CellClicked -= OnCellClicked;
    }

    public void SetStartValues(int startScore)
    {
        Score = startScore; 

        ScoreChanged?.Invoke(Score);
    }

    public void SetRecord(int record)
    {
        Record = record;
        RecordChanged?.Invoke(Record);
    }

    private void OnCellClicked()
    {
        Score++;

        if (Record < Score)
        {
            Record = Score;
            RecordChanged?.Invoke(Record);
        }

        ScoreChanged?.Invoke(Score);
    }

}
