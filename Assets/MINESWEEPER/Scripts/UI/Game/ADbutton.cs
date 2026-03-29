using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ADbutton : MonoBehaviour
{
    [SerializeField] private EndGameWindow _endGameWindow;
    [SerializeField] private ExitButton _exitButton;
    [SerializeField] private ExplodedCellRestorer _cellRestorer;

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
        _endGameWindow.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(true);
        _cellRestorer.RestoreCell();
    }
}
