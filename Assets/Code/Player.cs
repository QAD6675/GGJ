using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private bool grounded = false;
    private bool rolling =false;
    private Yarnball currentYarnBall;
    private Transform currentBallTransform;
    [SerializeField]private CatHUD hud;
    [SerializeField]private Animator animator;
    public float sizeChangeAmount = 3f; // Amount by which the cat's size increases
    public float minSize = 2f; // Minimum size limit
    public float maxSize = 10f; // Maximum size limit
    private Vector3 originalScale;
    [SerializeField]private DialogueManager dialogueManager;
    public BoxCollider2D collider;
    public float readTime=6f;
    public int currentlevel=1;
    private bool talking= false;
    [SerializeField] private Rigidbody2D rb;


private void IncreaseSize()
{
    Vector3 newScale = transform.localScale + new Vector3(sizeChangeAmount, sizeChangeAmount, 0);
    newScale.x = Mathf.Clamp(newScale.x, minSize, maxSize);
    newScale.y = Mathf.Clamp(newScale.y, minSize, maxSize);
    transform.localScale = newScale;
}

private void DecreaseSize()
{
    Vector3 newScale = transform.localScale - new Vector3(sizeChangeAmount, sizeChangeAmount, 0);
    newScale.x = Mathf.Clamp(newScale.x, minSize, maxSize);
    newScale.y = Mathf.Clamp(newScale.y, minSize, maxSize);
    transform.localScale = newScale;
}


    private void Start()
    {
        originalScale = transform.localScale; // Store the original size of the cat
        dialogueManager= GetComponent<DialogueManager>();//if you need it use saySomething(int);
    }
    public void triggerDialogue(int start,int end){
        talking=true;
        rb.velocity= new Vector2(0f,0f);
        dialogueManager.saySomething(start);
        if (start==end) {
            StartCoroutine(shutUp(true,start,end));
        }else{   
            StartCoroutine(shutUp(false,start,end));
        }
    }
    IEnumerator shutUp(bool last,int start,int end){
        yield return new WaitForSeconds(readTime);
        dialogueManager.clearDialogue();
        talking=false;
        yield return new WaitForSeconds(0.5f);
        if (!last) triggerDialogue(start+1,end);
    }
    public void Die(){
        hud.LoseLife();
        SceneManager.LoadScene("prototype");
    }
void Animate(){
    if (horizontal == 0f){
        animator.SetBool("walking", false);
    }else{
        animator.SetBool("walking", true);
    }
}

private void Update()
{
    if (talking){
        animator.SetBool("walking", false);
        return;
    }
    Animate();
    if (rolling)
    {
        transform.rotation = currentBallTransform.rotation;
        transform.position = currentBallTransform.position;
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
        if(rolling||talking)return;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.CompareTag("ground")){
            animator.SetBool("grounded", true);
            grounded = true;
        }
        if (obj.gameObject.CompareTag("dog")){
            Die();
        }
        if (obj.gameObject.CompareTag("yarnball")){
            currentYarnBall=obj.gameObject.GetComponent<Yarnball>();
            currentBallTransform=obj.gameObject.GetComponent<Transform>();
            animator.SetBool("trapped",true);
            collider.isTrigger = true;
            currentYarnBall.caughtCat();
            rolling =true;
            StartCoroutine("escape");
        }
    }
    IEnumerator escape(){
        yield return new WaitForSeconds(2f);
        rolling =false;
        animator.SetBool("trapped",false);
        collider.isTrigger=false; 
        currentYarnBall.releasetCat();
    }
    private void OnTriggerEnter2D(Collider2D obj) {
       if (obj.gameObject.tag == "dialogueTrigger") {
            dialogueTrigger dt =obj.gameObject.GetComponent<dialogueTrigger>();
            if (dt.spoken) return;
            dt.spoken=true;
            triggerDialogue(dt.start,dt.end);
        }
        if (obj.gameObject.CompareTag("PositiveCollectable"))
        {
            IncreaseSize();
            Destroy(obj.gameObject); // Destroy the collectable after collection
        }
        else if (obj.gameObject.CompareTag("NegativeCollectable"))
        {
            DecreaseSize();
            Destroy(obj.gameObject); // Destroy the collectable after collection
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