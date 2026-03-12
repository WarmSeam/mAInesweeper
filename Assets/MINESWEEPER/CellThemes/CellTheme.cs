using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell Theme", menuName = "Theme/New Cell Theme")]
public class CellTheme : ScriptableObject
{
    [SerializeField] private Sprite _closedImage;
    [SerializeField] private Sprite _emptyImage;
    [SerializeField] private Sprite _bombImage;
    [SerializeField] private Sprite _flagImage;

    public Sprite ClosedImage => _closedImage;
    public Sprite EmptyImage => _emptyImage;
    public Sprite BombImage => _bombImage;
    public Sprite FlagImage => _flagImage;

}
