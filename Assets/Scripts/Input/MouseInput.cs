using UnityEngine;

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
    private Vector3 _dragStartMousePos;
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
            _dragStartMousePos = Input.mousePosition;
            _dragStartWorldPos = _camera.ScreenToWorldPoint(_dragStartMousePos);
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

                Vector3 pos = _camera.transform.position;
                pos.x = Mathf.Clamp(pos.x, _minCameraPos.x, _maxCameraPos.x);
                pos.y = Mathf.Clamp(pos.y, _minCameraPos.y, _maxCameraPos.y);
                _camera.transform.position = pos;
            }

            _dragStartMousePos = Input.mousePosition;
            _dragStartWorldPos = _camera.ScreenToWorldPoint(_dragStartMousePos);
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
    #endregion

    #region Cell Input
    private void HandleCellInput()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (!_isDragging)
            {
                Cell cell = GetCellUnderPointer(Input.mousePosition);
                cell?.ToggleFlag();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cell cell = GetCellUnderPointer(Input.mousePosition);
            cell?.Open();
        }

        if (Input.GetMouseButtonDown(2))
        {
            Cell cell = GetCellUnderPointer(Input.mousePosition);
            cell?.OpenAround();
        }
    }

    private Cell GetCellUnderPointer(Vector3 screenPosition)
    {
        Vector3 world = _camera.ScreenToWorldPoint(screenPosition);
        Vector2 point = new Vector2(world.x, world.y);
        Collider2D hit = Physics2D.OverlapPoint(point);
        if (hit == null) return null;
        return hit.GetComponent<Cell>();
    }
    #endregion
}