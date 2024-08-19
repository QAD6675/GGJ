using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFollow : MonoBehaviour
{
    [SerializeField] Transform targetFollow;    // Target to follow
    [SerializeField] float xOffset = 5f;          // X-axis offset from target follow
    [SerializeField] float yOffset = 5f;           //y-axis offset from target follow
    [SerializeField] float smoothSpeed = 0.125f; // Speed of camera movement

    private void FixedUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = new Vector3(targetFollow.position.x, targetFollow.position.y, transform.position.z);
        desiredPosition = new Vector3(desiredPosition.x+xOffset, targetFollow.position.y+yOffset, desiredPosition.z);

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
