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
    private bool var =false;

    private void Start()
    {
        originalScale = transform.localScale;
        currentStretchValue = 1f;
        UpdateStretchSlider();
    }

    private void Update()
    {
        HandleSqueezeAndStretch();

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && currentStretchValue < 1f)
        {
            RegenerateStretchValue();
        }
    }

    private void HandleSqueezeAndStretch()
    {
        if (currentStretchValue > 0 && !isResetting)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StretchY(stretchSpeed);
                if (!var) DepleteStretchValue();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StretchY(-stretchSpeed);
                DepleteStretchValue();
            }
        }

        if (currentStretchValue <= 0)
        {
            isResetting = true;
            ResetScaleGradually();
        }
    }

    private void StretchY(float direction)
    {
        float tmp =transform.localScale.y;
        float newScaleY = Mathf.Clamp(transform.localScale.y + direction * Time.deltaTime, originalScale.y * minScaleY, originalScale.y * maxStretchY);
        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
        if(tmp == transform.localScale.y){
            var =true;
        }else
        {
            var= false;
        }
    }

private void ResetScaleGradually()
{
    transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * stretchSpeed);
    if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
    {
        isResetting = false;
        currentStretchValue = 1f;
        UpdateStretchSlider();
    }
}

    private void DepleteStretchValue()
    {
        currentStretchValue = Mathf.Clamp01(currentStretchValue - Time.deltaTime * (stretchSpeed / 10f));
        UpdateStretchSlider();
    }

private void RegenerateStretchValue()
{
    if (currentStretchValue <= 0)
    {
        isResetting = true;
        ResetScaleGradually();
    }
    else
    {
        currentStretchValue = Mathf.Clamp01(currentStretchValue + Time.deltaTime * (barRegenSpeed / 10f));
        UpdateStretchSlider();
    }
}

    private void UpdateStretchSlider()
    {
        stretchSlider.value = currentStretchValue;
    }
}
