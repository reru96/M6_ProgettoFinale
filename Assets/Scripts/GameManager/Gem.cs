using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject manager = GameObject.FindGameObjectWithTag("GemManager"); 
            if (manager != null)
            {
                manager.GetComponent<GemCollector>().CollectGem(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
