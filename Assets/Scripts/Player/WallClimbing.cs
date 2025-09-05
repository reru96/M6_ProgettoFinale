using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    private Rigidbody rb;
    [Header("References")]
    public Transform orientation; 
    public LayerMask whatIsWall;
    public Transform cameraTransform; 

    [Header("Climbing")]
    public float climbSpeed = 4f;
    public bool climbing;

    [Header("Detection")]
    public float detectionLength = 1f;
    public float sphereCastRadius = 0.5f;
    public float maxWallLookAngle = 60f;

    private float wallLookAngle;
    private RaycastHit frontWallHit;
    private bool wallFront;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing)
            Climb();
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);

        if (wallFront)
        {
            Vector3 camForwardFlat = cameraTransform.forward;
            camForwardFlat.y = 0;
            camForwardFlat.Normalize();

            wallLookAngle = Vector3.Angle(camForwardFlat, -frontWallHit.normal);
        }
    }

    private void StateMachine()
    {
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing)
                StartClimbing();
        }
        else
        {
            if (climbing)
                StopClimbing();
        }
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void Climb()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing()
    {
        climbing = false;
    }

}