using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellSaveData
{
    public int x;
    public int y;

    public bool isMine;
    public bool isOpened;

    public int minesAround;
}
