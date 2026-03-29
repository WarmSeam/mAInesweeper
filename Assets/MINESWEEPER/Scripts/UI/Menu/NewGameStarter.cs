using System;
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

    private Button _button;

    public event Action NewGameStarted;

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
        NewGameStarted?.Invoke();
        SceneManager.LoadScene(_sceneName);
    }
}
