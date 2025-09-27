using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private string gemSfx = "Gem";
    public GemSO gemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySfx(gemSfx);
            GameObject manager = GameObject.FindGameObjectWithTag("GemManager"); 
            if (manager != null)
            {
                manager.GetComponent<GemCollector>().CollectGem(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
