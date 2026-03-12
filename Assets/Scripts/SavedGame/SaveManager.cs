using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/save.json";
    }

    public void Save(GameSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    public GameSaveData Load()
    {
        if (!File.Exists(_path))
            return null;

        string json = File.ReadAllText(_path);

        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public bool HasSave()
    {
        return File.Exists(_path);
    }

    public void DeleteSave()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
}