using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public static SaveData pendingSaveData;

    protected override bool ShouldBeDestroyedOnLoad() => false;
    private void Start()
    {
        if (pendingSaveData != null)
        {

            pendingSaveData = null;
        }
    }
    public void SaveGame()
    {
        SaveData data = SaveManager.Load();

        data.sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        SaveManager.Save(data);
    }

}
