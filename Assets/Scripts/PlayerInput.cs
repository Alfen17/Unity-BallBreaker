using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerInput : MonoBehaviour  
{
    public static PlayerInput Instance { get; private set; }
    public string PlayerName { get; set; }
    public InputField nameInputField;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(SubmitName);
        startButton.onClick.AddListener(ChangeScene);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SubmitName()
    {

        PlayerInput.Instance.PlayerName = nameInputField.text;
        Debug.Log("Player Name set to: " + PlayerInput.Instance.PlayerName);

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("main");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); ;
    }


}
