using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private MineFiller _mineFiller;
    [SerializeField] private TextMeshProUGUI _text;

    private int _recordValue;
    private int _scoreValue;
    private float _difficultValue;


    private void OnEnable()
    {
        _score.ScoreChanged += OnScoreChanged;
        _score.RecordChanged += OnRecordChanged;

        _mineFiller.ChanceChanged += OnMineChanceChanged;
    }

    private void OnDisable()
    {
        _score.ScoreChanged -= OnScoreChanged;
        _score.RecordChanged -= OnRecordChanged;

        _mineFiller.ChanceChanged -= OnMineChanceChanged;
    }

    private void OnScoreChanged(int value)
    {
        _scoreValue = value;
        UpdateView();
    }

    private void OnRecordChanged(int value)
    {
        _recordValue = value;
        UpdateView();
    }

    private void OnMineChanceChanged(float value)
    {
        float percentDifficult = Mathf.RoundToInt(value * 100);
        _difficultValue = percentDifficult;
        UpdateView();
    }

    private void UpdateView()
    {
        _text.text = "Рекорд: " + _recordValue + "\nОчки: " + _scoreValue + "\nСложность: " + _difficultValue;
    }
}
