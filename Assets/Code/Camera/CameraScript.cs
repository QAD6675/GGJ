using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform targetFollow;    // Target to follow
    [SerializeField] Vector2 yClamp;            // Y-axis clamping limits
    [SerializeField] float smoothSpeed = 0.125f; // Speed of camera movement

    private void FixedUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = new Vector3(targetFollow.position.x, targetFollow.position.y, transform.position.z);

        // Clamp the desired position's y value
        float clampedY = Mathf.Clamp(desiredPosition.y, yClamp.x, yClamp.y);
        desiredPosition = new Vector3(desiredPosition.x, clampedY, desiredPosition.z);

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
