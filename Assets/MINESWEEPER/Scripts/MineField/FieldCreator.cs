using UnityEngine;

[RequireComponent(typeof(Field))]
public class FieldCreator : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _startSize = 5;

    private Field _field;

    private void Awake()
    {
        _field = GetComponent<Field>();
    }

    private void Start()
    {
        if(!_gameManager.TryLoadGame())
        GenerateStartField(_startSize);
    }

    private void GenerateStartField(int size)
    {
        _field.Expand(Vector2Int.zero, size);
    }
}
