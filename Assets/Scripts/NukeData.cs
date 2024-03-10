using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NukeData : MonoBehaviour
{
    public Button nuke;

    void Start()
    {
       
        nuke.onClick.AddListener(ClearGameData);
    }

    public void ClearGameData()
    {
        string path = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Game data cleared.");
        }
    }
}