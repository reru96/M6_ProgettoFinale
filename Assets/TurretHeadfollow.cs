using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadfollow : MonoBehaviour
{
    public Transform turretHead; 
    public Transform player;     
    public float rotationSpeed = 5f; 

    void Update()
    {
        if (player != null && turretHead != null)
        {
            Vector3 direction = player.position - turretHead.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
