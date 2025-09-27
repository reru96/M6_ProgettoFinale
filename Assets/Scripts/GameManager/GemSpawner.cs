using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform position;   
        public GameObject prefab;    
    }

    public SpawnPoint[] spawnPoints;

    void Start()
    {
        SpawnGems();
    }

    public void SpawnGems()
    {
        foreach (var sp in spawnPoints)
        {
            if (sp.position != null && sp.prefab != null)
            {
                Instantiate(sp.prefab, sp.position.position, sp.position.rotation);
            }
        }
    }
}

