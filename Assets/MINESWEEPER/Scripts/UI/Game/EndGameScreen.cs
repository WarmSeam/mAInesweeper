using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private TouchInput _touchInput;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _mouseInput.enabled = false;
        _touchInput.enabled = false;
    }

    private void OnDisable()
    {
        if(_mouseInput != null)
        _mouseInput.enabled = true; 

        if(_touchInput  != null)
        _touchInput.enabled = false;
    }
}
