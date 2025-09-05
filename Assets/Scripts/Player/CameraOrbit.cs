using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 5.0f;
    public float smoothSpeed = 10.0f;
    public Transform orientation;

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    public float pitchMin = -20f;
    public float pitchMax = 60f;

    private Quaternion currentRotation;

    void Start()
    {
        currentRotation = transform.rotation;
    }
    void LateUpdate()
    {
        if (target == null)
            return;

        float h = Input.GetAxis("Mouse X") * rotationSpeed;
        float v = -Input.GetAxis("Mouse Y") * rotationSpeed;

        currentYaw += h;
        currentPitch += v;
        currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);

        Quaternion desiredRotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        currentRotation = Quaternion.Slerp(currentRotation, desiredRotation, smoothSpeed * Time.deltaTime);


        Vector3 desiredPosition = target.position + currentRotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);

        if (orientation != null)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            orientation.forward = forward.normalized;
        }
    }


}


