using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SaveManager _saveManager;

    private Button _button;
    private bool _canContinued;

    public event Action Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        _canContinued = _saveManager.HasSave();

        UpdateState();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke();
    }

    private void UpdateState()
    {
        if (_canContinued == false)
        {
            Color disabledColor = _text.color;
            disabledColor.a -= 0.5f;
            _text.color = disabledColor;

            _button.interactable = false;
        }
    }

}
