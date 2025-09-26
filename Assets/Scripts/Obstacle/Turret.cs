using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected ObjectPoolManager poolManager;
    [SerializeField] protected Transform player;
    [SerializeField] protected float distance;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float nextFireTime;
    [SerializeField] protected float bulletMaxDistance;
    [SerializeField] protected float spreadAngle;
    [SerializeField] protected string turretSfx = "Shoot";
    //[SerializeField] protected GameObject zone;
    [SerializeField] protected VisioneCone visionCone;
    protected bool isRanged = false;


    protected virtual void Start()
    {
        visionCone = GetComponentInChildren<VisioneCone>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (visionCone.isVisible == true)
        {
            distance = Vector3.Distance(player.position, transform.position);
            if (distance <= maxDistance)
            {
                if (Time.time >= nextFireTime)
                {
                    AudioManager.Instance.PlaySfx(turretSfx);
                    Shoot(0f);
                    nextFireTime = Time.time + 1f / fireRate;
                }

                VanishActiveBullets();
            }
        }
    }

    protected virtual void VanishActiveBullets()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            if (bullet.gameObject.activeSelf)
            {
                if (Vector3.Distance(transform.position, bullet.transform.position) > bulletMaxDistance)
                {
                    bullet.gameObject.SetActive(false);
                }
            }
        }
    }

    protected virtual void Shoot(float spreadAngle)
    {
        GameObject bullet = poolManager.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
            bullet.gameObject.SetActive(true);
        }
    }

    //protected virtual void OnTriggerEnter(Collider other)
    //{

    //    if(other.CompareTag("Player"))
    //    { 
    //       isRanged = true;

    //    }
    //} 

    //protected virtual void OnTriggerExit(Collider other)
    //{

    //    if(other.CompareTag("Player"))
    //    {   
    //        isRanged = false;
    //    }
    //}
}
