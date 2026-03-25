using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    private string Path => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

    public void Save(GameSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);

        Debug.Log("Saved to: " + Path);
    }

    public GameSaveData Load()
    {
        if (!File.Exists(Path))
            return null;

        string json = File.ReadAllText(Path);

        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public bool HasSave()
    {
        return File.Exists(Path);
    }

    public void DeleteSave()
    {
        if (File.Exists(Path))
            File.Delete(Path);
    }
}