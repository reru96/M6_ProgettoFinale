using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int amountToPool = 10;

    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefabToPool, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        return null; 
    }
}
