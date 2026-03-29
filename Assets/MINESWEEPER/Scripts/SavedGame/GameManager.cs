using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] private Field _field;
    [SerializeField] private SaveDataSender _saveDataSender;
    [SerializeField] private SaveManager _saveManager;

    [Header("In Menu")]
    [SerializeField] private NewGameStarter _newGameStarter;

    private void OnEnable()
    {
        if (_field != null)
            _field.Updated += SaveGame;

        if(_newGameStarter != null)
        _newGameStarter.NewGameStarted += ClearSave;
    }

    private void OnDisable()
    {
        if (_field != null)
            _field.Updated -= SaveGame;

        if (_newGameStarter != null)
            _newGameStarter.NewGameStarted -= ClearSave;
    }

    public void SaveGame()
    {
        GameSaveData save = _saveDataSender.GetSaveData();
        RecordSaveData record = _saveDataSender.GetRecord();

        _saveManager.Save(save, record);
    }

    public bool TryLoadGame()
    {
        bool canLoad;

        GameSaveData save = _saveManager.LoadData();
        RecordSaveData record = _saveManager.LoadRecord();

        if (save == null)
        {
            canLoad = false;
        }
        else
        {
            canLoad = true;
            _saveDataSender.SetLoadData(save, record);
        }

        return canLoad;
    }

    public void ClearSave()
    {
        _saveManager.DeleteSave();
    }
}