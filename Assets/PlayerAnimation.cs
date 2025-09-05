using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private float lastYRotation;
    private bool doubleJumpTriggered = false;
    [SerializeField]private GroundChecker groundChecker;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        lastYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleMovementAnimations();
    }

    void HandleMovementAnimations()
    {
       
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = horizontalVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        Vector3 verticalVelocity = new Vector3(0, rb.velocity.y, 0);
        float vSpeed = horizontalVelocity.magnitude;
        animator.SetFloat("VSpeed", vSpeed);

        bool isGrounded = playerMovement.groundChecker.IsGroundedAny();
        animator.SetBool("IsGrounded", isGrounded);

        float turnValue = 0f;
        if (speed < 0.1f)
        {
            float currentY = transform.eulerAngles.y;
            float delta = Mathf.DeltaAngle(lastYRotation, currentY);
            turnValue = Mathf.Clamp(delta / (Time.deltaTime * 180f), -1f, 1f);
        }
        //animator.SetFloat("Turn", turnValue);
        lastYRotation = transform.eulerAngles.y;

      
        if (!isGrounded && rb.velocity.y > 0.1f)
        {
           
            if (playerMovement.JumpCount == 1 && !doubleJumpTriggered)
            {
                animator.SetTrigger("DoubleJump");
                doubleJumpTriggered = true;
            }
        }

        if (isGrounded)
        {
            doubleJumpTriggered = false;
        }

        animator.SetBool("IsFalling", rb.velocity.y < -0.1f);

        if (isGrounded && rb.velocity.y < 0f)
            animator.SetTrigger("Land");

        //if (playerMovement.state != PlayerMovement.MovementState.wallrunning)
        //    animator.SetBool("IsWallRunning", false);

        //if (playerMovement.state != PlayerMovement.MovementState.wallstick)
        //    animator.SetBool("IsWallSticking", false);
    }
}
