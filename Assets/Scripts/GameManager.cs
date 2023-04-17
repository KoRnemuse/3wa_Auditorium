using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gamehasEnded = false;
    private float TimeWhenRestart;
    [SerializeField] float TimeAfterDeadToRestart = 5f;
    [SerializeField] float TimeRemaining = 5;
    public bool TimerIsRunning = false;

    [SerializeField]
    public TextMeshProUGUI TimeText;

    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject LevelCompletedPanel;

    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
        LevelCompletedPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerIsRunning)
        {
            if (gamehasEnded == true)
            {
                if (Time.time >= TimeWhenRestart)
                {
                    Restart();
                    Debug.Log("Time to restart");
                    TimeRemaining = 0;
                    TimerIsRunning = false;
                }
                else if (TimeRemaining > 0)
                {
                    TimeRemaining -= Time.deltaTime;
                    DisplayTime(TimeRemaining);
                }
            }
        }
    }

    public void GameOver()
    {
        if (gamehasEnded == false)
        {
            gamehasEnded = true;
            GameOverPanel.SetActive(true);
            TimeWhenRestart = Time.time + TimeAfterDeadToRestart;
            TimerIsRunning = true;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0); //Load first scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DisplayTime(float TimeToDisplay)
    {
        TimeToDisplay += 1;
        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);
        //_levelNumberTextMesh.text = $"Level:{SceneManager.GetActiveScene().buildIndex}";
        Debug.Log("TESSST");
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LevelComplete()

    {
        LevelCompletedPanel.SetActive(true);
        FindObjectOfType<CameraRays>().enabled = false;

    }
}