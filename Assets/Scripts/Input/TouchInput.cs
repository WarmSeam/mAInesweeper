using UnityEngine;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 50f;
    [SerializeField] private float _dragThreshold = 0.01f;
    [SerializeField] private float _holdTimeForFlag = 0.4f;
    [SerializeField] private Vector2 _minCameraPos = new Vector2(-50, -50);
    [SerializeField] private Vector2 _maxCameraPos = new Vector2(50, 50);

    private Vector3 _dragStartWorld;
    private float _holdTimer;
    private bool _isDragging;
    private bool _flagTriggered;
    private float _lastPinchDist;

    private int _tapCount;
    private float _tapTimer;
    private const float doubleTapTime = 0.3f;

    private void Update()
    {
        if (Input.touchCount == 1)
            HandleSingleTouch();
        else if (Input.touchCount == 2)
            HandlePinchDrag();
        else
            ResetTouchStates();
    }

    private void HandleSingleTouch()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 worldPos = _camera.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            _dragStartWorld = worldPos;
            _isDragging = false;
            _holdTimer = 0;
            _flagTriggered = false;

            _tapCount++;
            if (_tapCount == 1) _tapTimer = 0f;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (Vector3.Distance(worldPos, _dragStartWorld) > _dragThreshold)
                _isDragging = true;
        }

        if (touch.phase == TouchPhase.Stationary && !_flagTriggered)
        {
            _holdTimer += Time.deltaTime;
            if (_holdTimer >= _holdTimeForFlag && !_isDragging)
            {
                Cell cell = GetCellUnderPointer(touch.position);
                cell?.ToggleFlag();
                _flagTriggered = true;
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            if (!_isDragging && !_flagTriggered)
            {
                if (_tapCount == 2 && _tapTimer <= doubleTapTime)
                {
                    Cell cell = GetCellUnderPointer(touch.position);
                    cell?.OpenAround();
                    _tapCount = 0;
                }
                else
                {
                    Cell cell = GetCellUnderPointer(touch.position);
                    cell?.Open();
                }
            }
        }

        if (_tapCount == 1)
        {
            _tapTimer += Time.deltaTime;
            if (_tapTimer > doubleTapTime) _tapCount = 0;
        }
    }

    private void HandlePinchDrag()
    {
        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);

        // Drag камерой средним движением
        Vector2 delta = (t0.deltaPosition + t1.deltaPosition) / 2f;
        Vector3 startScreenPos = new Vector3(t0.position.x - delta.x, t0.position.y - delta.y, 0f);
        Vector3 startWorld = _camera.ScreenToWorldPoint(startScreenPos);
        Vector3 endWorld = _camera.ScreenToWorldPoint(t0.position);
        _camera.transform.position -= (endWorld - startWorld);

        Vector3 pos = _camera.transform.position;
        pos.x = Mathf.Clamp(pos.x, _minCameraPos.x, _maxCameraPos.x);
        pos.y = Mathf.Clamp(pos.y, _minCameraPos.y, _maxCameraPos.y);
        _camera.transform.position = pos;

        _isDragging = true;

        // Zoom
        float dist = Vector2.Distance(t0.position, t1.position);
        if (_lastPinchDist != 0)
        {
            float deltaDist = dist - _lastPinchDist;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - deltaDist * 0.01f * _zoomSpeed, _minZoom, _maxZoom);
        }
        _lastPinchDist = dist;
    }

    private void ResetTouchStates()
    {
        _isDragging = false;
        _lastPinchDist = 0;
    }

    private Cell GetCellUnderPointer(Vector3 screenPosition)
    {
        Vector3 world = _camera.ScreenToWorldPoint(screenPosition);
        Vector2 point = new Vector2(world.x, world.y);
        Collider2D hit = Physics2D.OverlapPoint(point);
        return hit ? hit.GetComponent<Cell>() : null;
    }
}