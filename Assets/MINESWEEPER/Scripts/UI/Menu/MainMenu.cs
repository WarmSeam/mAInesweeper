using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NewGameButton _newGame;
    [SerializeField] private ContinueButton _continue;
    [SerializeField] private NewGameWindow _newGameWindow;

    private void OnEnable()
    {
        _continue.Clicked += OnContinueClicked;
        _newGame.Clicked += OnNewGameClicked;
    }

    private void OnDisable()
    {
        _continue.Clicked -= OnContinueClicked;
        _newGame.Clicked -= OnNewGameClicked;
    }

    private void UpdateButtons()
    {

    }

    private void OnContinueClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnNewGameClicked()
    {
        _newGameWindow.gameObject.SetActive(true);
    }

    private void OnOptionsClicked()
    {

    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
