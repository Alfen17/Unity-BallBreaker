using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text ScoreText;
    public Text NameText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        SaveData loadedData = GameDataHandler.LoadGameData();

        Debug.Log("Loaded game data: " + JsonUtility.ToJson(loadedData));

        if (loadedData != null)
            
        HighScoreText.text = "Highscore: " +  loadedData.highScore + " by " + loadedData.playerName;
            
        

        ShowName();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);

                


            }
        }
    }

    private void Update()
    
    
    
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Indlæs den eksisterende gemte data for at sammenligne highscores
        SaveData loadedData = GameDataHandler.LoadGameData();

        // Tjek om den aktuelle score er højere end den gemte highscore
        if (loadedData == null || m_Points > loadedData.highScore)
        {
            // Hvis ja, opdater highscoren og gem den nye data
            SaveData data = new SaveData
            {
                playerName = PlayerInput.Instance.PlayerName,
                highScore = m_Points
            };

            GameDataHandler.SaveGameData(data);
            Debug.Log("Saving new high score: " + JsonUtility.ToJson(data));
            HighScoreText.text = "Highscore: " + m_Points + " by " + PlayerInput.Instance.PlayerName;
        }
        else
        {
            Debug.Log("Current score is not higher than the high score. No update needed.");
            Debug.Log("Current points: " + m_Points);
            Debug.Log("Loaded high score: " + loadedData.highScore);
        }
    }


    void ShowName()
    {
        NameText.text = ("Name:" + PlayerInput.Instance.PlayerName);
    }
  
 

 
}
