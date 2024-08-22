using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private Vector3 initialScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float animationSpeed = 5f;

    public void pop()
    {
        gameObject.SetActive(true);
        transform.localScale = initialScale;
        StartCoroutine(AnimatePopup(targetScale));
    }

    private IEnumerator AnimatePopup(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * animationSpeed);
            yield return null;
        }
        transform.localScale = target;
    }

    public void close()
    {
        StartCoroutine(AnimatePopup(Vector3.zero));
    }

    private void OnDisable()
    {
        // Reset scale to ensure it starts correctly next time it's enabled
        transform.localScale = initialScale;
    }
}

