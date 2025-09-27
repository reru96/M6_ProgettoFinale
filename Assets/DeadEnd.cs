using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnd : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        LifeController life = other.gameObject.GetComponent<LifeController>();
        life.AddHp(-5);  
        GameObject collectorObj = GameObject.FindGameObjectWithTag("GemManager");
        GemCollector collector = collectorObj.GetComponent<GemCollector>();
        Vector3 lastPos = collector.GetLastGemPosition();

       
        other.transform.position = lastPos + Vector3.up * 2;
      
    }
}




