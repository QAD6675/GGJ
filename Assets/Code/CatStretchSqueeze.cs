using UnityEngine;

public class CatStretchSqueeze : MonoBehaviour
{
    public float stretchSpeed = 2f;  
    public float maxStretchY = 2f;   
    public float minScaleY = 0.5f;   
    private Vector3 originalScale;   

    void Start()
    {
       
        originalScale = transform.localScale;
    }

    void Update()
    {
       
        HandleSqueezeAndStretch();
    }

    private void HandleSqueezeAndStretch()
    {
       
        if (Input.GetKey(KeyCode.W))
        {
            StretchY(stretchSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            StretchY(-stretchSpeed);
        }
        else
        {
            // Reset the scale when no squeezing or stretching should happen
            ResetScale();
        }
    }

    // Function to squeeze/stretch along the y-axis
    void StretchY(float direction)
    {
        float newScaleY = Mathf.Clamp(transform.localScale.y + direction * Time.deltaTime, originalScale.y * minScaleY, originalScale.y * maxStretchY);
        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
    }

    // Function to reset the scale to original values
    void ResetScale()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            Mathf.Lerp(transform.localScale.y, originalScale.y, Time.deltaTime * stretchSpeed),
            transform.localScale.z);
    }
}