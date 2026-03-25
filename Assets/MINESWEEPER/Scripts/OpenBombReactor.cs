using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBombReactor : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ExitButton _exitButton;
    [SerializeField] private float _delayTime = 1f;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delayTime);
    }

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
        _endGameScreen.DisableInput();
        _exitButton.gameObject.SetActive(false);

        StartCoroutine(OpenEndScreen());
    }

    private IEnumerator OpenEndScreen()
    {
        yield return _wait;
        _endGameScreen.gameObject.SetActive(true);
    }
}
