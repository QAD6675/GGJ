using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatStretchSqueeze : MonoBehaviour
{
    
    public float stretchSpeed = 2f;  
    public float maxStretchY = 2f;   
    public float minScaleY = 0.5f;   
    public float barRegenSpeed = 1f; 
    public Text stretchText;         

    private Vector3 originalScale;  
    private float currentStretchValue; 
    private bool isResetting = false; 
    void Start()
    {
        originalScale = transform.localScale;
        currentStretchValue = 1f; 
        UpdateStretchText();
    }

    void Update()
    {
        // Handle squeezing/stretching along the y-axis only if not resetting
        if (!isResetting)
        {
            HandleSqueezeAndStretch();
        }

        // Regenerate the stretch value when not squeezing/stretching
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            RegenerateStretchValue();
        }
    }

    private void HandleSqueezeAndStretch()
    {
        // Check if there is any stretch value left
        if (currentStretchValue > 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StretchY(stretchSpeed);
                DepleteStretchValue();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StretchY(-stretchSpeed);
                DepleteStretchValue();
            }
        }

        // If stretch value is depleted or over max value, reset the player's size gradually
        if (currentStretchValue <= 0 || currentStretchValue >= 1f)
        {
            isResetting = true;
            ResetScaleGradually();
        }
    }

    private void StretchY(float direction)
    {
        float newScaleY = Mathf.Clamp(transform.localScale.y + direction * Time.deltaTime, originalScale.y * minScaleY, originalScale.y * maxStretchY);
        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
    }

    private void ResetScaleGradually()
    {
        // Gradually reset the scale to the original size
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * stretchSpeed);

        // Check if the reset is complete
        if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
        {
            isResetting = false;
        }
    }

    private void DepleteStretchValue()
    {
        // Deplete the value when squeezing or stretching
        currentStretchValue = Mathf.Clamp(currentStretchValue - Time.deltaTime * stretchSpeed, 0, 1f);
        UpdateStretchText();
    }

    private void RegenerateStretchValue()
    {
        // Regenerate the value gradually when not in use
        currentStretchValue = Mathf.Clamp(currentStretchValue + Time.deltaTime * barRegenSpeed, 0, 1f);
        UpdateStretchText();
    }

    private void UpdateStretchText()
    {
        // Update the stretch value display
        stretchText.text = $"Stretch Value: {currentStretchValue:F2}";
    }

}