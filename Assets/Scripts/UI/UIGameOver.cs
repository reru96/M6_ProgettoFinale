using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject endMenu;
    public GameObject winMenu;

    private void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        endMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0.5f;
        StartCoroutine(ShowWinMenuDelayed());
    }

    public void ShowDefeat()
    {
        defeatPanel.SetActive(true);
        Time.timeScale = 0.5f;
        StartCoroutine(ShowEndMenuDelayed());
    }

    private IEnumerator ShowEndMenuDelayed()
    {
        yield return new WaitForSeconds(1f);
        ShowEndMenu();
    }
    
    private IEnumerator ShowWinMenuDelayed()
    {
        yield return new WaitForSeconds(1f);
        ShowWinMenu();
    }

    private void ShowEndMenu()
    {
        Time.timeScale = 0f;
        endMenu.SetActive(true);
    }
    private void ShowWinMenu()
    {
        Time.timeScale = 0f;
        winMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void NextScene()
    {
       Time.timeScale = 1f; 
       int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
       if (nextScene < SceneManager.sceneCountInBuildSettings)
       {
          SceneManager.LoadScene(nextScene);
       }
    }
   
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
