using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
 
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private bool grounded = false;
    private bool rolling =false;
    [SerializeField]private Transform currentBallTransform;

    [SerializeField] private Rigidbody2D rb;
    void Update()
    {
        if (rolling){
            Debug.Log("snapping..");
            transform.rotation=currentBallTransform.rotation;
            transform.localScale=currentBallTransform.localScale;
            transform.position=currentBallTransform.position;
            Flip();
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.4f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.CompareTag("ground")){
            grounded= true;
        }
        if (obj.gameObject.CompareTag("yarnball")){
            currentBallTransform;
            rolling =true;
        }
    }
    private void OnCollisionExit2D(Collision2D obj) {
        if (obj.gameObject.CompareTag("ground"))
        {
            grounded=false;
        }
        if (obj.gameObject.CompareTag("yarnball")){
            rolling =false;
        }
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
