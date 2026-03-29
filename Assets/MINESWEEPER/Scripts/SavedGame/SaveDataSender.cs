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

    public RecordSaveData GetRecord()
    {
        return new() { Record = _counter.Record };
    }

    public void SetLoadData(GameSaveData loadedSave, RecordSaveData loadedRecord)
    {
        _mineFiller.SetLoadChance(loadedSave.MineChance);
        _counter.SetStartValues(loadedSave.Score, loadedRecord.Record);
        _field.SetLoadedData(loadedSave);
    }
}
