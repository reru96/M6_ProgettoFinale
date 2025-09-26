using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : Turret
{
    private Transform zone;
    private Renderer mat;

    protected override void Start()
    {
        base.Start();
        if (zone != null)
        {
            mat = zone.GetComponent<Renderer>();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mat != null)
        {
            mat.material.color = Color.red;
            isRanged = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && mat != null)
        {
            mat.material.color = Color.white;
            isRanged = false;
        }
    }
}
