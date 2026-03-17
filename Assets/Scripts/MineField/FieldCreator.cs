using UnityEngine;

[RequireComponent(typeof(FieldRegulator))]
public class FieldCreator : MonoBehaviour
{
    [SerializeField] private int _startSize = 5;

    private FieldRegulator _field;

    private void Awake()
    {
        _field = GetComponent<FieldRegulator>();
    }

    private void Start()
    {
        GenerateStartField(_startSize);
    }

    private void GenerateStartField(int size)
    {
        _field.Expand(Vector2Int.zero, size);
    }
}
