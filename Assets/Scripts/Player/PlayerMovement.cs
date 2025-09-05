using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float jumpForce;
    public float airMultiplier;
    
    [Header("Ground Check")]
    public GroundChecker groundChecker;

    public Transform orientation;

    private int jumpCount;
    public int JumpCount => jumpCount;

    private int maxJumpCount = 1;
    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;
    public MovementState state;

    public enum MovementState
    {
        
        walking,
        sprinting,
        air,
        wallrunning,
        wallstick
    }

    public bool wallrunning;
    public bool wallstick;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
       

        ResetJump();
        MyInput();
        SpeedControl();
        StateHandler();
        
        rb.drag = groundChecker.IsGroundedAny() ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 direction = new Vector3(moveDirection.x, 0f, moveDirection.z);

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void MyInput()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            Jump();
            jumpCount++;
        }

    }

    private void StateHandler()
    {

        if (groundChecker.IsGroundedAny() && Input.GetButtonDown("Run"))
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }
        else if (groundChecker.IsGroundedAny())
        {
            state = MovementState.walking;
            speed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        
        if (groundChecker.IsGroundedAny())
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        else if (!groundChecker.IsGroundedAny())
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

       
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        if (groundChecker.IsGroundedAny()) jumpCount = 0;
    }
}