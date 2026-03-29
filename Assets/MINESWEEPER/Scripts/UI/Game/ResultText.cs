using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResultText : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"Заработано очков: {_scoreCounter.Score}\nРекорд: {_scoreCounter.Record}";
    }
}
