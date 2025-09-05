using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHook2 : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 hookPoint;
    public LayerMask hookable;
    public Transform tip, player;
    [SerializeField] private Camera cam; 
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float spring = 10f;
    [SerializeField] private float damper = 7f;
    [SerializeField] private float massScale = 4f;
    [SerializeField] private float minDistanceRatio = 0.25f;
    [SerializeField] private float maxDistanceRatio = 0.8f;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryHook();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopHook();
        }

        if (joint)
        {
            Vector3 directionToHook = (hookPoint - player.position).normalized;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(directionToHook * 20f, ForceMode.Acceleration); 
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void TryHook()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hookable))
        {
            hookPoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hookPoint;

            float distanceFromPoint = Vector3.Distance(player.position, hookPoint);

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            joint.maxDistance = distanceFromPoint * maxDistanceRatio;
            joint.minDistance = distanceFromPoint * minDistanceRatio;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, tip.position);
        lr.SetPosition(1, hookPoint);
    }

    void StopHook()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

}
