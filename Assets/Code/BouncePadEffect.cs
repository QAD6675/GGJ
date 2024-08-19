using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadEffect : MonoBehaviour
{
    [SerializeField] float BounceStrength = 1500f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            rb.velocityY = 0f;
            rb.AddForce(transform.up * BounceStrength, ForceMode2D.Impulse);
        }
    }
}
