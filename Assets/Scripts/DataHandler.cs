using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class GameDataHandler
{
    private static string dataPath = Application.persistentDataPath + "/gameData.json";

    public static void SaveGameData(SaveData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath, jsonData);
    }

    public static SaveData LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<SaveData>(jsonData);
        }
        return new SaveData();
    }
}

