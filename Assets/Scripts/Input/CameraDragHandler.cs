using UnityEngine;

public class CameraDragHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _dragSpeed = 1f;

    private Vector3 _lastMousePos;

    public void HandleDrag()
    {
        if (Input.GetMouseButtonDown(1))
            _lastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - _lastMousePos;
            _camera.transform.Translate(-delta * _dragSpeed * Time.deltaTime);
            _lastMousePos = Input.mousePosition;
        }
    }

    public void HandleTouchDrag()
    {
        if (Input.touchCount != 2)
            return;

        Vector2 delta = Input.GetTouch(0).deltaPosition;
        _camera.transform.Translate(-delta * _dragSpeed * Time.deltaTime);
    }
}