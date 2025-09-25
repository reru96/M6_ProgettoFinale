using UnityEngine;
using static PlayerMovement;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Referenze")]
    [SerializeField] private PlayerMovement player; 
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;


    private Vector3 lastVelocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();    

    }

    private void Update()
    {
        if (!player) return;

        UpdateMovementAnimation();
        UpdateJumpAnimation();
        UpdateFallAnimation();
        UpdateLandingAnimation();
    }

    private void UpdateMovementAnimation()
    {
    
        Vector3 flatVel = new Vector3(player.Rb.velocity.x, 0f, player.Rb.velocity.z);
        float speed = flatVel.magnitude;

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", player.groundChecker.IsGroundedAny());
    }

    private void UpdateJumpAnimation()
    {
       
        animator.SetBool("IsJumping", player.isJumping);

      
        if (player.state == PlayerMovement.MovementState.DoubleJump)
        {
            animator.SetTrigger("DoubleJump");
        }

    }

    private void UpdateFallAnimation()
    {
        
        bool isInAir = !player.groundChecker.IsGroundedAny();
        bool isFalling = player.Rb.velocity.y < -0.1f;

        animator.SetBool("IsFalling", isInAir && isFalling);
    }

    private void UpdateLandingAnimation()
    {
   
        bool wasInAir = lastVelocity.y > 0.1f || lastVelocity.y < -0.1f;
        bool isGrounded = player.groundChecker.IsGroundedAny();

        if (wasInAir && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        lastVelocity = player.Rb.velocity;
    }

}
