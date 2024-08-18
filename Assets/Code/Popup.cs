using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePopup : MonoBehaviour
{
    public float shrinkDuration = 0.4f;
    private Vector3 initialScale;

    private void OnEnable()
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

    // Wait for the shrink duration
    yield return new WaitForSeconds(shrinkDuration);

    // Start shrinking the popup
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
