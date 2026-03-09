using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellView : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Sprite _closedImage;
    [SerializeField] private Sprite _emptyImage;
    [SerializeField] private Sprite _bombImage;
    [SerializeField] private Sprite _flagImage;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _closedImage;

        _text.text = null;
    }

    private void OnEnable()
    {
        _cell.Empted += OnEmpted;
        _cell.Opened += OnOpened;
        _cell.Exploded += OnExploded;
        _cell.Flagged += OnFlagged;
    }

    private void OnDisable()
    {
        _cell.Empted -= OnEmpted;
        _cell.Opened -= OnOpened;
        _cell.Exploded -= OnExploded;
    }

    private void OnEmpted()
    {
        _renderer.sprite = _emptyImage;
        if (_text != null)
            _text.text = string.Empty;
    }

    private void OnOpened(int obj)
    {
        _renderer.sprite = _emptyImage;
        _text.text = "" + _cell.MinesAroundCount;
    }

    private void OnExploded()
    {
        _renderer.sprite = _bombImage;

        if (_text != null)
            _text.text = string.Empty;
    }

    private void OnFlagged(bool isStanded)
    {
        if(isStanded)
        _renderer.sprite = _flagImage;
        else
            _renderer.sprite = _closedImage;

        if (_text != null)
            _text.text = string.Empty;
    }
}
