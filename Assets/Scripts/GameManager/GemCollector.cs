
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

    void Start()
    {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        collectedGems = 0;

        if (portal != null)
            portal.SetActive(false);
     
        foreach (GameObject gemUI in gemUIPrefab)
        {
            if (gemUI != null)
                gemUI.SetActive(false);
        }
    }

    public void CollectGem(GameObject gem)
    {
        Destroy(gem);
        collectedGems++;

       
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

    public void EnterPortal()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
