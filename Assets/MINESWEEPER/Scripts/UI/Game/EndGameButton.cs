using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EndGameButton : MonoBehaviour
{
    [SerializeField] private string _menuSceneName = "Menu";
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
        SceneManager.LoadScene(_menuSceneName);
    }
}
