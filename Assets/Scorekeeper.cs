using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] int score;
    const int DEFAULT_POINTS = 1;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {
        score = PersistentData.Instance.GetScore();
        level = PersistentData.Instance.GetLevel();

        //display score
        DisplayScore();
        DisplayLevel();
        DisplayName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int addend)
    {
        score += addend;
        Debug.Log("score" + score);
        //display score
        DisplayScore();
        PersistentData.Instance.SetScore(score);
    }

    public void UpdateScore()
    {
        UpdateScore(DEFAULT_POINTS);
    }

    private void DisplayScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void DisplayLevel()
    {
        levelText.text = "Level " + level;
    }

    public void DisplayName()
    {
        nameText.text = "Hi, " + PersistentData.Instance.GetName();
    }
}
