using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpread : Turret
{
   [SerializeField] private ObjectPoolManager poolS;
    protected override void Update()
    {
        if (!isRanged) return;

        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= maxDistance)
        {
          
            if (Time.time >= nextFireTime)
            {
                Vector3 baseDirection = (player.position - firePoint.position).normalized;

                ShootWithDirection(baseDirection, 0f);              
                ShootWithDirection(baseDirection, +spreadAngle);   
                ShootWithDirection(baseDirection, -spreadAngle);    

                nextFireTime = Time.time + 1f / fireRate;
            }

            VanishActiveBullets();
        }
    }

    // Nuovo metodo per gestire la direzione
    private void ShootWithDirection(Vector3 baseDirection, float angle)
    {
        GameObject bullet = poolS.GetPooledObject();
        if (bullet == null) return;

        Quaternion spreadRot = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 finalDir = (spreadRot * baseDirection).normalized;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(finalDir);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(finalDir * bulletSpeed, ForceMode.Impulse);
        }

        bullet.SetActive(true);
    }

}
