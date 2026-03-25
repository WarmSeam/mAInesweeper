using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("SceneManager")]
    [SerializeField] private string _gameSceneName = "Game";
    [SerializeField] private SaveManager _saveManager;

    [Header ("Buttons")]
    [SerializeField] private NewGameButton _newGame;
    [SerializeField] private ContinueButton _continue;

    [Header("Windows")]
    [SerializeField] private NewGameWindow _newGameWindow;

    private void OnEnable()
    {
        if (!_saveManager.HasSave())
            _continue.DisableClick();

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
        SceneManager.LoadScene(_gameSceneName);
    }

    private void OnNewGameClicked()
    {
        if (_saveManager.HasSave())
            _newGameWindow.gameObject.SetActive(true);
        else
            SceneManager.LoadScene(_gameSceneName);

    }

    private void OnOptionsClicked()
    {

    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
