using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        if(pauseButton == null) pauseButton = GameObject.Find("PauseButton");
        if(resumeButton == null) resumeButton = GameObject.Find("ResumeButton");
        Time.timeScale = 1.0f;

        resumeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;

        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
    }
}
