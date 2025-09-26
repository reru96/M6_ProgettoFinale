using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnd : MonoBehaviour
{
    //public ParticleSystem fogEffect;

    //public void Start()
    //{
    //    fogEffect.Play();
    //}
    private void OnCollisionEnter(Collision other)
    {
        LifeController life = other.gameObject.GetComponent<LifeController>();
        life.SetHp(0);
    }
    //private void OnCollisionExit(Collision other)
    //{

    //    if (fogEffect)
    //        fogEffect.Stop();

    //}

}
