
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GemCollector : MonoBehaviour
{
    public GameObject portal;
    public string nextSceneName = "Level2";
    public Sprite gemSprite;
    public Transform firstRespawnPoint;

    public Transform gemPanel;     

    private int totalGems;
    private int collectedGems;
    private Vector3 lastGemPosition;

    public int CollectedGems => collectedGems;

    void Start()
    {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
       
        collectedGems = 0;

        SaveData data = SaveManager.Load();
        collectedGems = data.collectedGems;

        if (portal != null)
            portal.SetActive(collectedGems >= totalGems);

    }

    public void CollectGem(GameObject gemObj)
    {
        Gem gem = gemObj.GetComponent<Gem>();
        if (gem == null) return;

        lastGemPosition = gemObj.transform.position;
        Destroy(gemObj);
        collectedGems++;

        Instantiate(gem.gemData.gemPrefabUI, gemPanel);
        

        if (collectedGems >= totalGems && portal != null)
        {
            portal.SetActive(true);
        }

        SaveData data = new SaveData { collectedGems = collectedGems };
        SaveManager.Save(data);
    }

    public Vector3 GetLastGemPosition()
    {
        if (collectedGems == 0)
        {
            Debug.Log("numero di gemme: " + collectedGems);

            lastGemPosition = firstRespawnPoint.transform.localPosition;
            
        }
        return lastGemPosition;
    }
}
