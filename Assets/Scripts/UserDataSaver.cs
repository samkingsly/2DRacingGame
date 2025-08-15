using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class UserDataSaver
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "userdata.json");

    public static void Save(UserDataSO data)
    {
        string json = JsonUtility.ToJson(data, true); // pretty print
        File.WriteAllText(SavePath, json);
        Debug.Log($"UserData saved to {SavePath}");
    }

    public static void Load(UserDataSO data)
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(json, data);
            Debug.Log($"UserData loaded from {SavePath}");
        }
        else
        {
            Debug.Log("No save file found — using default ScriptableObject values.");
        }
    }

}
