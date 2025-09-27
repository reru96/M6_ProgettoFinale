
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GemCollector : MonoBehaviour
{
    public GameObject portal;
    public string nextSceneName = "Level2";
    public Sprite gemSprite;

    public GameObject[] gemUIPrefab;   

    private int totalGems;
    private int collectedGems;

    public int CollectedGems => collectedGems;

    void Start()
    {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;

        SaveData data = SaveManager.Load();
        collectedGems = data.collectedGems;

        if (portal != null)
            portal.SetActive(collectedGems >= totalGems);

       
        for (int i = 0; i < collectedGems && i < gemUIPrefab.Length; i++)
        {
            if (gemUIPrefab[i] != null)
            {
                gemUIPrefab[i].SetActive(true);
                Image img = gemUIPrefab[i].GetComponent<Image>();
                if (img != null && gemSprite != null)
                {
                    img.sprite = gemSprite;
                    img.enabled = true;
                }
            }
        }
    }

    public void CollectGem(GameObject gem)
    {
        Destroy(gem);
        collectedGems++;

       
        SaveData data = new SaveData { collectedGems = collectedGems };
        SaveManager.Save(data);

        if (gemUIPrefab != null && collectedGems <= gemUIPrefab.Length)
        {
            GameObject gemUI = gemUIPrefab[collectedGems - 1];
            if (gemUI != null)
            {
                gemUI.SetActive(true);

                Image img = gemUI.GetComponent<Image>();
                if (img != null && gemSprite != null)
                {
                    img.sprite = gemSprite;
                    img.enabled = true;
                }
            }
        }

        if (collectedGems >= totalGems && portal != null)
        {
            portal.SetActive(true);
        }
    }
}
