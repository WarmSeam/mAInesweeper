using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    private string ValuesPath => System.IO.Path.Combine(Application.persistentDataPath, "data.json");
    private string RecordPath => System.IO.Path.Combine(Application.persistentDataPath, "record.json");

    public void Save(GameSaveData data, RecordSaveData record)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(ValuesPath, jsonData);

        string jsonRecord = JsonUtility.ToJson(record, true);
        File.WriteAllText(RecordPath, jsonRecord);

        Debug.Log("Saved");
    }

    public GameSaveData LoadData()
    {
        if (!File.Exists(ValuesPath))
            return null;

        string json = File.ReadAllText(ValuesPath);

        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public RecordSaveData LoadRecord()
    {
        if (!File.Exists(RecordPath))
            return null;

        string json = File.ReadAllText(RecordPath);

        Debug.Log("сейв звгружен");
        return JsonUtility.FromJson<RecordSaveData>(json);

    }

    public bool HasSave()
    {
        return File.Exists(ValuesPath);
    }

    public void DeleteSave()
    {
        if (File.Exists(ValuesPath))
            File.Delete(ValuesPath);
    }
}