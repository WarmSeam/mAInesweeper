using UnityEngine;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private TouchInput _touchInput;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HandleEnable();
    }

    private void OnDisable()
    {
        HandleDisable();
    }

    public void HandleEnable()
    {
        if (_mouseInput != null)
            _mouseInput.enabled = false;

        if (_touchInput != null)
            _touchInput.enabled = false;
    }

    public void HandleDisable()
    {
        if (_mouseInput != null)
            _mouseInput.enabled = true;

        if (_touchInput != null)
            _touchInput.enabled = true;
    }


}
