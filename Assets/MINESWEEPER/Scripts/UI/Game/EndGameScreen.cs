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
       DisableInput();
    }

    private void OnDisable()
    {
        if(_mouseInput != null)
        _mouseInput.enabled = true; 

        if(_touchInput  != null)
        _touchInput.enabled = false;
    }

    public void DisableInput()
    {
        _mouseInput.enabled = false;
        _touchInput.enabled = false;
    }
}
