using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatStretchSqueeze : MonoBehaviour
{
    
    // Stretch and Squeeze variables
    public float stretchSpeed = 2f;  // Speed at which the player stretches/squeezes
    public float maxStretchY = 2f;   // Maximum stretch along the y-axis
    public float minScaleY = 0.5f;   // Minimum scale factor (squeeze factor)
    public float barRegenSpeed = 1f; // Speed at which the bar regenerates when not used
    public Slider stretchSlider;     // UI Slider for displaying the stretch value

    private Vector3 originalScale;   // Original scale of the player
    private float currentStretchValue; // Current stretch value (normalized between 0 and 1)
    private bool isResetting = false; // Flag to check if the player is resetting to original size

    void Start()
    {
        // Store the original scale of the player
        originalScale = transform.localScale;
        // Initialize the stretch value
        currentStretchValue = 1f; // 1 represents the full stretch capacity
        UpdateStretchSlider();
    }

    void Update()
    {
        // Handle squeezing/stretching along the y-axis only if not resetting
        HandleSqueezeAndStretch();

        // Regenerate the stretch value when not squeezing/stretching
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && currentStretchValue < 1f)
        {
            RegenerateStretchValue();
        }
    }

    private void HandleSqueezeAndStretch()
    {
        // Check if there is any stretch value left
        if (currentStretchValue > 0 && !isResetting)
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

        // If stretch value is depleted, reset the player's size gradually
        if (currentStretchValue <= 0)
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
            currentStretchValue = 1f;  // Reset the stretch value to full after returning to normal size
            UpdateStretchSlider();
        }
    }

    private void DepleteStretchValue()
    {
        // Deplete the value when squeezing or stretching
        currentStretchValue = Mathf.Clamp01(currentStretchValue - Time.deltaTime * (stretchSpeed / 10f));
        UpdateStretchSlider();
    }

    private void RegenerateStretchValue()
    {
        // Regenerate the value gradually when not in use
        currentStretchValue = Mathf.Clamp01(currentStretchValue + Time.deltaTime * (barRegenSpeed / 10f));
        UpdateStretchSlider();
    }

    private void UpdateStretchSlider()
    {
        // Update the stretch value display on the slider
        stretchSlider.value = currentStretchValue;
    }

}