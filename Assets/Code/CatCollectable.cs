using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCollectable : MonoBehaviour
{
    public float sizeIncreaseAmount = 0.2f; // Amount by which the cat's size increases
    public float minSize = 0.5f; // Minimum size limit
    public float maxSize = 2.0f; // Maximum size limit

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale; // Store the original size of the cat
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PositiveCollectable"))
        {
            IncreaseSize();
            Destroy(collision.gameObject); // Destroy the collectable after collection
        }
        else if (collision.gameObject.CompareTag("NegativeCollectable"))
        {
            DecreaseSize();
            Destroy(collision.gameObject); // Destroy the collectable after collection
        }
    }

    private void IncreaseSize()
    {
        Vector3 newScale = transform.localScale + new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, 0);
        transform.localScale = new Vector3(Mathf.Clamp(newScale.x, minSize, maxSize), Mathf.Clamp(newScale.y, minSize, maxSize), newScale.z);
    }

    private void DecreaseSize()
    {
        Vector3 newScale = transform.localScale - new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, 0);
        transform.localScale = new Vector3(Mathf.Clamp(newScale.x, minSize, maxSize), Mathf.Clamp(newScale.y, minSize, maxSize), newScale.z);
    } 
}
