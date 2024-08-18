using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public float readDuration = 5f;
    private Vector3 initialScale;

    public void pop()
    {
        // Initialize the initial scale of the popup
        initialScale = transform.localScale;
        // Start the animation
        StartCoroutine(AnimatePopup());
    }

private IEnumerator AnimatePopup()
{
        Vector3 targetScale = initialScale*10;

        // Loop until the popup is fully shrunk
        while (transform.localScale != targetScale)
        {
            // Shrink the popup towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);

            yield return null;
        }
    // Ensure the popup is fully scaled at the end of the animation
    transform.localScale = targetScale;
}

public  void close(){
    StartCoroutine(ShrinkPopup());
}
    private IEnumerator ShrinkPopup()
    {
        // Calculate the target scale for shrinking
        Vector3 targetScale = Vector3.zero;

        // Loop until the popup is fully shrunk
        while (transform.localScale != targetScale)
        {
            // Shrink the popup towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);

            yield return null;
        }

        // Disable the popup after shrinking
        gameObject.SetActive(false);
    }
}