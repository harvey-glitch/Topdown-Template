using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // The player or target the camera will follow
    [SerializeField] Transform target;
    // Smoothing factor for smooth camera movement
    [SerializeField] float smoothSpeed = 0.125f;
    // Flag to enable smoothing effect
    [SerializeField] bool smoothen = false;

    // Camera offset
    Vector3 offset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Calculate the initial offset based on the starting position of target and this object
        offset = transform.position - target.transform.position;
    }


    void LateUpdate()
    {
        if (target == null)
            return;

        SmoothFollow();
    }

    void SmoothFollow()
    {
        // Desired position is the target's position + offset
        Vector3 desiredPosition = target.position + offset;

        if (smoothen)
            // Smooth the movement
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        else
            // Directly set the position
            transform.position = desiredPosition;
    }
}
