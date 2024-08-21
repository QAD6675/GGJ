using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private float oldPosition;

    void Awake()
    {
        oldPosition = transform.position.x;
    }

    void Update()
    {
        float newPosition = transform.position.x;
        if (newPosition != oldPosition)
        {
            // Calculate the movement delta as new - old.
            float deltaMovement = newPosition - oldPosition;
            onCameraTranslate?.Invoke(deltaMovement);
            oldPosition = newPosition;
        }
    }
}

