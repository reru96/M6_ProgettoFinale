using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Settings")]
    public float checkRadius = 0.2f;
    public float offsetY = 0.6f;
    public LayerMask whatIsGround;
    public LayerMask whatIsBuilding;

    public bool Grounded { get; private set; }
    public bool GroundedBuilding { get; private set; }

    private void FixedUpdate()
    {
        Vector3 checkPosition = transform.position + Vector3.down * offsetY;
        Grounded = Physics.CheckSphere(checkPosition, checkRadius, whatIsGround, QueryTriggerInteraction.Ignore);
        GroundedBuilding = Physics.CheckSphere(checkPosition, checkRadius, whatIsBuilding, QueryTriggerInteraction.Ignore);
    }

    public bool IsGroundedAny()
    {
        return Grounded || GroundedBuilding;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Grounded || GroundedBuilding ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + Vector3.down * offsetY;
        Gizmos.DrawWireSphere(checkPosition, checkRadius);
    }
}
