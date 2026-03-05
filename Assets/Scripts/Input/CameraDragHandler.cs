using UnityEngine;

public class CameraDragHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _dragSpeed = 1f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 50f;

    private Vector3 _lastMousePos;
    private float _lastTouchDistance;

    private void Update()
    {
        HandleDrag();
        HandleTouchDrag();
        HandleZoom();
        HandleTouchZoom();
    }

    #region Drag
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

        // Средняя точка между пальцами для движения
        Vector2 touch0Delta = Input.GetTouch(0).deltaPosition;
        Vector2 touch1Delta = Input.GetTouch(1).deltaPosition;
        Vector2 delta = (touch0Delta + touch1Delta) / 2f;

        _camera.transform.Translate(-delta * _dragSpeed * Time.deltaTime);
    }
    #endregion

    #region Zoom
    private void HandleZoom()
    {
        // ПК колесико
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            float newSize = _camera.orthographicSize - scroll * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
        }
    }

    private void HandleTouchZoom()
    {
        if (Input.touchCount != 2)
            return;

        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);

        float currentDistance = Vector2.Distance(t0.position, t1.position);
        if (_lastTouchDistance == 0)
        {
            _lastTouchDistance = currentDistance;
            return;
        }

        float delta = currentDistance - _lastTouchDistance;
        float newSize = _camera.orthographicSize - delta * 0.01f * _zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);

        _lastTouchDistance = currentDistance;
    }
    #endregion
}