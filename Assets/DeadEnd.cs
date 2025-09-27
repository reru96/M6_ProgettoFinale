using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnd : MonoBehaviour
{
    public Transform firstRespawnPoint;

    private void OnCollisionEnter(Collision other)
    {
        LifeController life = other.gameObject.GetComponent<LifeController>();
        life.AddHp(-5);  
        GameObject collectorObj = GameObject.FindGameObjectWithTag("GemManager");
        GemCollector collector = collectorObj.GetComponent<GemCollector>();
        Vector3 lastPos = collector.GetLastGemPosition();

        if (collector.CollectedGems > 0)
        {

            other.transform.position = lastPos + Vector3.up * 2;
        }
        else
        {
            other.transform.position = firstRespawnPoint.position;
        }
    }
}




