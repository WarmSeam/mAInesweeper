using UnityEngine;

public class PlayInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CameraDragHandler _cameraDrag;

    private float _touchHoldTime = 0.4f;
    private float _touchTimer;
    private bool _isHolding;

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cell cell = GetCellUnderPointer(Input.mousePosition);
            cell?.Open();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cell cell = GetCellUnderPointer(Input.mousePosition);
            cell?.ToggleFlag();
        }

        if (Input.GetMouseButtonDown(2))
        {
            Cell cell = GetCellUnderPointer(Input.mousePosition);
            cell?.OpenAround();
        }

        if (Input.GetMouseButton(1))
        {
            _cameraDrag.HandleDrag();
        }
    }

    private void HandleTouch()
    {
        if (Input.touchCount == 2)
        {
            _cameraDrag.HandleTouchDrag();
            return;
        }

        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            _touchTimer = 0f;
            _isHolding = true;
        }

        if (touch.phase == TouchPhase.Stationary && _isHolding)
        {
            _touchTimer += Time.deltaTime;

            if (_touchTimer >= _touchHoldTime)
            {
                Cell cell = GetCellUnderPointer(touch.position);
                cell?.ToggleFlag();
                _isHolding = false;
            }
        }

        if (touch.phase == TouchPhase.Ended && _isHolding)
        {
            Cell cell = GetCellUnderPointer(touch.position);
            cell?.Open();
        }
    }

    private Cell GetCellUnderPointer(Vector3 screenPosition)
    {
        Vector3 world = _camera.ScreenToWorldPoint(screenPosition);
        Vector2 point = new Vector2(world.x, world.y);

        Collider2D hit = Physics2D.OverlapPoint(point);

        if (hit == null)
            return null;

        return hit.GetComponent<Cell>();
    }
}