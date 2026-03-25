using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 50f;

    [Header("Camera Drag Settings")]
    [SerializeField] private float _dragThreshold = 0.01f;
    [SerializeField] private Vector2 _minCameraPos = new Vector2(-50, -50);
    [SerializeField] private Vector2 _maxCameraPos = new Vector2(50, 50);

    private bool _isDragging;
    private Vector3 _dragStartWorldPos;

    private void Update()
    {
        HandleDrag();
        HandleZoom();
        HandleCellInput();
    }

    #region Camera Drag & Zoom
    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dragStartWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _isDragging = false;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currentWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 deltaWorld = currentWorldPos - _dragStartWorldPos;

            if (deltaWorld.magnitude > _dragThreshold)
                _isDragging = true;

            if (_isDragging)
            {
                _camera.transform.position -= deltaWorld;
                ClampCamera();
            }

            _dragStartWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            float newSize = _camera.orthographicSize - scroll * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
        }
    }

    private void ClampCamera()
    {
        Vector3 pos = _camera.transform.position;
        pos.x = Mathf.Clamp(pos.x, _minCameraPos.x, _maxCameraPos.x);
        pos.y = Mathf.Clamp(pos.y, _minCameraPos.y, _maxCameraPos.y);
        _camera.transform.position = pos;
    }
    #endregion

    #region Cell Input
    private void HandleCellInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mousePos = Input.mousePosition;
        Cell cell = GetCellUnderPointer(mousePos);

        if (Input.GetMouseButtonUp(1))
        {
            if (!_isDragging)
                cell?.ToggleFlag();
        }

        if (Input.GetMouseButtonDown(0))
        {
            cell?.Open();
        }

        if (Input.GetMouseButtonDown(2))
        {
            cell?.OpenAround();
        }
    }

    private Cell GetCellUnderPointer(Vector3 screenPosition)
    {
        Vector2 world = _camera.ScreenToWorldPoint(screenPosition);
        Collider2D hit = Physics2D.OverlapPoint(world);
        return hit ? hit.GetComponent<Cell>() : null;
    }
    #endregion
}