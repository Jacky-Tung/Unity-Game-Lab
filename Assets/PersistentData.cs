using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{

    [SerializeField] int playerScore;
    [SerializeField] string playerName;
    [SerializeField] int level;

    public static PersistentData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        playerName = "";
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string s)
    {
        playerName = s;
    }

    public void SetScore(int s)
    {
        playerScore = s;
    }

    public void SetLevel(int s)
    {
        level = s;
    }

    public string GetName()
    {
        return playerName;
    }

    public int GetScore()
    {
        return playerScore;
    }

    public int GetLevel()
    {
        return level;
    }
}