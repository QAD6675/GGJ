using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform targetFollow;
    [SerializeField] private Vector2 yClamp;

    private void Update()
    {
        Vector3 newPosition = new Vector3(targetFollow.position.x, transform.position.y, transform.position.z);
        newPosition.y = Mathf.Clamp(newPosition.y, yClamp.x, yClamp.y);
        transform.position = newPosition;
    }
}
