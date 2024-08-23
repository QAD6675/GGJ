using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator animator;
    private void Start() {
        animator=GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("collided",true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("collided",false);
        }
    }
}
