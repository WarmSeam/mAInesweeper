using System.Collections;
using UnityEngine;

public class OpenBombReactor : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private ExplodedCellRestorer _cellRestorer;
    [SerializeField] private EndGameWindow _endGameScreen;
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

    private void OnBombOpened(Cell cell)
    {
        _endGameScreen.HandleEnable();
        _exitButton.gameObject.SetActive(false);

        _cellRestorer.SetCell(cell);

        StartCoroutine(OpenEndScreen());
    }

    private IEnumerator OpenEndScreen()
    {
        yield return _wait;
        _endGameScreen.gameObject.SetActive(true);
    }
}
