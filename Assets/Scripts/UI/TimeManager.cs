using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float timeLeft = 60f;
    private bool levelCompleted = false;

    public TMP_Text timerText; 

    void Update()
    {
        if (levelCompleted)
            return;

        timeLeft -= Time.deltaTime;

  
        if (timeLeft < 0f)
            timeLeft = 0f;

        
        UpdateTimerUI();

        if (timeLeft <= 0f)
        {
            GameOver();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Livello completato!");
    }

    void GameOver()
    {
        Debug.Log("Tempo scaduto! Hai perso.");
        FindObjectOfType<UIGameOver>().ShowDefeat();
    }
}

