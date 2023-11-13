using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void PlayGame()
    {
        PersistentData.Instance.SetName(playerNameInput.text);
        SceneManager.LoadScene((PersistentData.Instance.GetLevel() + 1)); 
    }

    public void MainMenu()
    {
        if(SceneManager.GetActiveScene().name == "finish"){
            PersistentData.Instance.SetScore(0);
            PersistentData.Instance.SetLevel(1);    
        }
        SceneManager.LoadScene("GameMenu");
    }

    public void PlayAgain()
    {
        PersistentData.Instance.SetScore(0);
        PersistentData.Instance.SetLevel(1);
        SceneManager.LoadScene("level1");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }
}
