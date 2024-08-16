using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform TargetFollow;

    [SerializeField] Vector2 YClamp;

    private void Update()
    {
        transform.position = TargetFollow.position;

        float yPosition = Mathf.Clamp(transform.position.y, YClamp.x, YClamp.y);
        transform.position = new Vector2(transform.position.x, yPosition);
    }
}
