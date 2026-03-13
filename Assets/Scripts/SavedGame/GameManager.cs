using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private MineFiller _mineFiller;
    [SerializeField] private SaveManager _saveManager;

    private void OnEnable()
    {
        _field.Updated += SaveGame;
    }
    private void OnDisable()
    {
        _field.Updated -= SaveGame;
    }

    public void SaveGame()
    {
        GameSaveData save = _field.GetSaveData(_score.Score);
        _saveManager.Save(save);
    }

    public void LoadGame()
    {
        GameSaveData save = _saveManager.Load();

        if (save == null)
            return;

        _field.LoadFromSave(save);

        _score.SetStartValue(save.Score);
    }
}