using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    private SaveData lastSave;

    private void Start()
    {
        settingsMenu.SetActive(false);


        lastSave = SaveManager.Load();


        if (lastSave == null || lastSave.sceneIndex == 0)
        {
            lastSave = null;
        }
    }

    public void NewGame()
    {

        SaveData newSave = new SaveData();
        newSave.collectedGems = 0;
        newSave.sceneIndex = 1;
        newSave.playerX = 0f;
        newSave.playerY = 0f;
        newSave.playerZ = 0f;
       


        SaveManager.Save(newSave);

        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        if (lastSave != null)
        {

            SceneManager.LoadScene(lastSave.sceneIndex);

            GameController.pendingSaveData = lastSave;
        }
        else
        {
            Debug.Log("Nessun salvataggio trovato. Avvio nuova partita...");
            NewGame();
        }
    }

    public void ShowOptions()
    {
        settingsMenu.SetActive(true);
    }

    public void HideOptions()
    {
        settingsMenu.SetActive(false);
    }

    public void RollCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
