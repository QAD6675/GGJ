using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatStretchSqueeze : MonoBehaviour
{
    public float stretchSpeed = 2f;
    public float maxStretchY = 2f;
    public float minScaleY = 0.5f;
    public float barRegenSpeed = 1.4f;
    public Slider stretchSlider;

    private Vector3 originalScale;
    private float currentStretchValue;
    private bool isResetting = false;

    private void Start()
    {
        originalScale = transform.localScale;
        currentStretchValue = 1f; // Start with a full stretch bar
        UpdateStretchSlider();
    }

    private void Update()
    {
        // Handle squeezing/stretching
        if (!isResetting)
        {
            HandleSqueezeAndStretch();
        }

        // Regenerate stretch value when not stretching/squeezing
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && currentStretchValue < 1f)
        {
            RegenerateStretchValue();
        }
    }

    private void HandleSqueezeAndStretch()
    {
        // Stretch/squeeze if there's stretch value left
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

        // Start resetting if the stretch value is depleted
        if (currentStretchValue <= 0)
        {
            StartCoroutine(ResetScaleGradually());
        }
    }

    private void StretchY(float direction)
    {
        float newScaleY = Mathf.Clamp(transform.localScale.y + direction * Time.deltaTime, originalScale.y * minScaleY, originalScale.y * maxStretchY);
        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
    }

    private IEnumerator ResetScaleGradually()
    {
        isResetting = true;

        while (Vector3.Distance(transform.localScale, originalScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * stretchSpeed);
            yield return null;
        }

        // Reset complete, restore the stretch value and stop resetting
        transform.localScale = originalScale;
        currentStretchValue = 1f;
        UpdateStretchSlider();
        isResetting = false;
    }

    private void DepleteStretchValue()
    {
        currentStretchValue = Mathf.Clamp01(currentStretchValue - Time.deltaTime * (stretchSpeed / 2f)); // Adjusted depletion speed
        UpdateStretchSlider();
    }

    private void RegenerateStretchValue()
    {
        currentStretchValue = Mathf.Clamp01(currentStretchValue + Time.deltaTime * (barRegenSpeed / 2f)); // Adjusted regeneration speed
        UpdateStretchSlider();
    }

    private void UpdateStretchSlider()
    {
        stretchSlider.value = currentStretchValue;
    }
}