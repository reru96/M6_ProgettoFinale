using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementLeftToRight : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public float waitTime = 5f;
    [SerializeField] private float moveSpeed = 5f;

    private float timer;
    private bool movingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = Vector3.left;
        timer = waitTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
   
            movingLeft = !movingLeft;
            moveDirection = movingLeft ? Vector3.left : Vector3.right;
            timer = waitTime;
        }

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
