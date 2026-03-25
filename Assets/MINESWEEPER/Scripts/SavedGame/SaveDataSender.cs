using UnityEngine;

public class SaveDataSender : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private MineFiller _mineFiller;

    public GameSaveData GetSaveData()
    {
        return new()
        {
            Score = _counter.Score,
            MineChance = _mineFiller.MineChance,
            CellDatas = _field.GetCellsData()
        };
    }

    public void SetLoadData(GameSaveData loadedSave)
    {
        _mineFiller.SetLoadChance(loadedSave.MineChance);
        _counter.SetStartValue(loadedSave.Score);
        _field.SetLoadedData(loadedSave);
    }
}
