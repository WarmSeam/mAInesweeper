using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellView : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CellTheme _theme;

    private Sprite _closedImage;
    private Sprite _emptyImage;
    private Sprite _bombImage;
    private Sprite _flagImage;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        SetSprites();

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
        _cell.Flagged -= OnFlagged;
    }

    private void OnEmpted()
    {
        _renderer.sprite = _emptyImage;
        if (_text != null)
            _text.text = string.Empty;
    }

    private void OnOpened(int minesCount)
    {
        _renderer.sprite = _emptyImage;

        if(minesCount > 0)
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

    public void SetSprites()
    {
        _closedImage = _theme.ClosedImage;
        _emptyImage = _theme.EmptyImage;
        _bombImage = _theme.BombImage;
        _flagImage = _theme.FlagImage;
    }
}
