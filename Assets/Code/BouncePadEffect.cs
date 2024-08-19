using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadEffect : MonoBehaviour
{
    [SerializeField] float BounceStrength = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("spring")){
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocityY = 0f;
            rb.AddForce(transform.up * BounceStrength, ForceMode2D.Impulse);
        }
    }
}
