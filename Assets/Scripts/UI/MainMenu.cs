using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _options;
    [SerializeField] private Button _quit;

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _continue.onClick.AddListener(OnContinueClicked);
        _newGame.onClick.AddListener(OnNewGameClicked);
        _options.onClick.AddListener(OnOptionsClicked);
        _quit.onClick.AddListener(OnQuitClicked);

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        _continue.interactable = _saveManager.HasSave();
    }

    private void OnContinueClicked()
    {
        _gameManager.LoadGame();
    }

    private void OnNewGameClicked()
    {
        _saveManager.DeleteSave();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnOptionsClicked()
    {
        Debug.Log("Options clicked");
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}