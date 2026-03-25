using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NewGameStarter : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private SaveManager _saveManager;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
       
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _saveManager.DeleteSave();
        SceneManager.LoadScene(_sceneName);
    }
}
