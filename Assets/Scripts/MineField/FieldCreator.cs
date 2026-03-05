using UnityEngine;

[RequireComponent(typeof(MineFiller))]
public class FieldCreator : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private int _startSize = 5;

    private void Start()
    {
        GenerateStartField(_startSize);
    }

    private void GenerateStartField(int size)
    {
        _field.Expand(Vector2Int.zero, size);
    }
}
