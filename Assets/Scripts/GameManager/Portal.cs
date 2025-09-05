using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject manager = GameObject.FindGameObjectWithTag("GemManager");
            if (manager != null)
            {
                manager.GetComponent<GemCollector>().EnterPortal();
            }
        }
    }
}
