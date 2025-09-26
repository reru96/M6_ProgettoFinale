using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisioneCone : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 45f;
    public int horizontalRayCount = 20;
    public int verticalRayCount = 10;
    public LayerMask targetLayer;
    public bool isVisible;
   
    void Update()
    {
        CastCone();

    }

    public bool CastCone()
    {
        float halfAngle = viewAngle / 2;
        isVisible = false;

        for (int v = 0; v <= verticalRayCount; v++)
        {
            float pitch = -halfAngle + (viewAngle / verticalRayCount) * v;

          
          
            for (int h = 0; h <= horizontalRayCount; h++)
            {
                float yaw = -halfAngle + (viewAngle / horizontalRayCount) * h;

                Vector3 dir = Quaternion.Euler(pitch, yaw, 0) * transform.forward;

               
                if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewDistance, targetLayer))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    return isVisible = true;
                }
                else
                {
                    Debug.DrawRay(transform.position, dir * viewDistance, Color.green);
                }
            }

        }
        return isVisible;
    }

}


