using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cell : MonoBehaviour
{
    public bool IsMined { get; private set; }
    public bool IsFlagged { get; private set; }
    public bool IsOpen { get; private set; }
    public Vector2Int Position { get; private set; }
    public int MinesAroundCount { get; private set; }
    public int NeighbourCount { get; private set; }

    private Field _field;

    public event Action<Cell> OnCellClicked;
    public event Action Exploded;
    public event Action Empted;
    public event Action<bool> Flagged;
    public event Action<int> Opened;

    private void Awake()
    {
        IsOpen = false;
        IsFlagged = false;
    }

    public void Initialize(Vector2Int position, Field field)
    {
        Position = position;
        _field = field;

        IsOpen = false;
        IsFlagged = false;
        IsMined = false;
    }

    public void PlaceMine() => IsMined = true;
    public void SetNeighboursCount(int count) => NeighbourCount = count;
    public void SetMinesAroundCount(int count) => MinesAroundCount = count;

    public void ToggleFlag()
    {
        if (IsOpen)
            return;

        IsFlagged = !IsFlagged;

        Flagged?.Invoke(IsFlagged);
    }

    public void Open()
    {
        if (IsOpen || IsFlagged)
            return;

        Queue<Cell> queue = new Queue<Cell>();

        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            Cell current = queue.Dequeue();

            if (current.IsOpen)
                continue;

            current.IsOpen = true;

            current.OnCellClicked?.Invoke(current);

            if (current.IsMined)
            {
                current.Exploded?.Invoke();
                continue;
            }

            if (current.MinesAroundCount == 0)
            {
                current.Empted?.Invoke();

                foreach (Cell neighbour in _field.GetNeighbours(current.Position))
                    if (!neighbour.IsOpen && !neighbour.IsFlagged && !neighbour.IsMined)
                        queue.Enqueue(neighbour);
            }
            else
            {
                current.Opened?.Invoke(current.MinesAroundCount);
            }
        }
    }

    public void OpenAround()
    {
        var neighbours = _field.GetNeighbours(Position);
        int flaggedCount = neighbours.Count(n => n.IsFlagged);

        if (flaggedCount == MinesAroundCount && IsOpen)
        {
            foreach (var neighbour in neighbours)
                neighbour.Open();
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 scale = Vector3.one * 0.9f;

        Gizmos.DrawWireCube(transform.position, scale);
    }
#endif
}