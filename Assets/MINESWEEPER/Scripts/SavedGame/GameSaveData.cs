using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int Record;
    public int Score;
    public float MineChance;
    public List<CellSaveData> CellDatas = new List<CellSaveData>();
}
