using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float groundDrag = 6f;
    public float jumpForce = 7f;
    public float acceleration = 10f;
    public float airMultiplier = 0.5f;

    [Header("Jump")]
    public int maxJumpCount = 1;
    public float coyoteTime = 0.2f;


    public Transform orientation;
    public GroundChecker groundChecker;

    private Rigidbody rb;
    private int jumpCount;
    private float coyoteTimeCounter;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    public bool isJumping;

    public MovementState state;

    public Rigidbody Rb => rb;

    public enum MovementState
    {
        Walking,
        Sprinting,
        Air,
        Jump,
        DoubleJump,
        Land
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleInput();
        HandleState();
        HandleJumpReset();
        ApplyDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount || coyoteTimeCounter > 0f))
        {
            if (jumpCount == 2)
                isJumping = false;
                state = MovementState.DoubleJump;

            jumpCount++;
            isJumping = true;  
            Jump();

        }
    }

    private void HandleState()
    {

        switch (state)
        {
            case MovementState.Walking:
            case MovementState.Sprinting:
                if (!groundChecker.IsGroundedAny())
                {
                    state = MovementState.Air;
                }
                else if (Input.GetButton("Run"))
                {
                    state = MovementState.Sprinting;
                }
                else
                {
                    state = MovementState.Walking;
                }
                break;

            case MovementState.DoubleJump:
                if (rb.velocity.y < 0f)
                {
                    state = MovementState.Air;
                }
                break;

            case MovementState.Air:
                if (groundChecker.IsGroundedAny())
                {
                    state = MovementState.Land;
                    isJumping = false;
                }
                break;

            case MovementState.Land:
                if (groundChecker.IsGroundedAny())
                {
                    if (Input.GetButton("Run"))
                        state = MovementState.Sprinting;
                    else
                        state = MovementState.Walking;
                }
                break;

            default:
                state = MovementState.Walking;
                break;

        }
    }

    private void HandleJumpReset()
    {
        if (groundChecker.IsGroundedAny())
        {
            coyoteTimeCounter = coyoteTime;
            jumpCount = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void ApplyDrag()
    {
        rb.drag = groundChecker.IsGroundedAny() ? groundDrag : 0f;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float targetSpeed = (state == MovementState.Sprinting) ? sprintSpeed : walkSpeed;
        Vector3 force = moveDirection.normalized * targetSpeed * acceleration;

        if (!groundChecker.IsGroundedAny())
            force *= airMultiplier;

        rb.AddForce(force, ForceMode.Force);

        LimitSpeed(targetSpeed);
    }

    private void LimitSpeed(float targetSpeed)
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > targetSpeed)
        {
            Vector3 limitedVel = Vector3.Lerp(flatVel, flatVel.normalized * targetSpeed, Time.deltaTime * acceleration);
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void RotatePlayer()
    {
        Vector3 direction = new Vector3(moveDirection.x, 0f, moveDirection.z);
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * acceleration);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}