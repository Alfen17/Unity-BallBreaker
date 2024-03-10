using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        SaveData loadedData = GameDataHandler.LoadGameData();

        Debug.Log("Loaded game data: " + JsonUtility.ToJson(loadedData));

        if (loadedData != null)

            highScoreText.text = "Highscore:" + loadedData.playerName + " " + loadedData.highScore;
    }

 
}