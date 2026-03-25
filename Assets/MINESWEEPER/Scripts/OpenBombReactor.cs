using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBombReactor : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private EndGameScreen _endGameScreen;

    private void OnEnable()
    {
        _field.BombOpened += OnBombOpened;
    }

    private void OnDisable()
    {
        _field.BombOpened -= OnBombOpened;
    }

    private void OnBombOpened()
    {
        _endGameScreen.gameObject.SetActive(true);
    }
}
