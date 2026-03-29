using System.Collections.Generic;

[System.Serializable]
public class GameSaveData
{
    public int Record;
    public int Score;
    public float MineChance;
    public List<CellSaveData> CellDatas = new();
}
