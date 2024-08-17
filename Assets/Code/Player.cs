using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    [SerializeField]private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    void Animate(){
        if (horizontal == 0f){
            animator.SetBool("walking", false);
        }else{
            animator.SetBool("walking", true);
        }
    }

private void Update()
{
    Animate();
    if (rolling)
    {
        transform.rotation = currentBallTransform.rotation;
        transform.position = currentBallTransform.position;
        Flip();
        return;
    }

    if (Input.GetButtonDown("Jump") && grounded)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        animator.SetBool("grounded", false);
        grounded = false;
    }

    if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
    }

    float newHorizontal = Input.GetAxisRaw("Horizontal");
    if (newHorizontal != horizontal)
    {
        horizontal = newHorizontal;
        Flip();
    }
}

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.CompareTag("ground")){
            animator.SetBool("grounded", true);
            grounded = true;
        }
        if (obj.gameObject.CompareTag("yarnball")){
            currentBallTransform=obj.gameObject.GetComponent<Transform>();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rolling =true;
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