using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public dataStore data;
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
    [SerializeField]private DialogueManager dialogueManager;
    public int lives;
    public BoxCollider2D collider;
    public float readTime=6f;
    public int currentlevel=1;
    private bool talking= false;
    [SerializeField] private Rigidbody2D rb;

void Start(){
    lives=data.lives;
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
        if (lives==1){
            SceneManager.LoadScene("level 1");
            Destroy(data);
        }
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