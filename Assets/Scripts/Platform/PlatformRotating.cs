using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotating : MonoBehaviour
{
    private Rigidbody rb;
    public float waitTime = 5f;
    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    [SerializeField] private float roll;    

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

   

    void FixedUpdate()
    {
            Quaternion rotation = Quaternion.Euler(roll * Time.fixedDeltaTime,yaw * Time.fixedDeltaTime, pitch * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * rotation);
    }
    

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.transform.SetParent(this.transform);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.transform.SetParent(null);
    //    }
    //}
}
