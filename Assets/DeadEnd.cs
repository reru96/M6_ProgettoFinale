using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnd : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
       LifeController life = other.gameObject.GetComponent<LifeController>();
       life.AddHp(-1000);
    }
}
