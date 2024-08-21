using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float horizontal;
    public CircleCollider2D cc;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private bool grounded = false;
    private bool rolling =false;
    [HideInInspector]public bool hasKey=false;
    [SerializeField]private CatHUD hud;
    [SerializeField]private Animator animator;
    public float sizeChangeAmount = 3f; // Amount by which the cat's size increases
    public float minSize = 2f; // Minimum size limit
    public float maxSize = 10f; // Maximum size limit
    private Vector3 originalScale;
    [SerializeField]private DialogueManager dialogueManager;
    public BoxCollider2D bc;
    public float readTime=6f;
    public int currentLevel;
    private bool right;
    private bool talking= false;
    [SerializeField] private Rigidbody2D rb;
    public AudioClip[] catVoice; // Array to hold game tracks
    public AudioSource audioSource;
public void PlayAudio(string str)
{
    Dictionary<string, int[]> soundRanges = new Dictionary<string, int[]>
    {
        {"purr", new int[]{12, 14}},
        {"meow", new int[]{1, 10}},
        {"gurr", new int[]{10, 12}},
        {"jump", new int[]{14, 14}},
        {"win", new int[]{15, 15}},
        {"die", new int[]{0, 0}}
    };
    if (soundRanges.TryGetValue(str, out int[] range))
    {
        int i = Random.Range(range[0], range[1]);
        audioSource.clip = catVoice[i];
        audioSource.Play();
    }
}

public void AdjustSize(bool increase)
{
    float adjustment = increase ? sizeChangeAmount : -sizeChangeAmount;
    Vector3 newScale = transform.localScale + new Vector3(adjustment, adjustment, 0);
    newScale.x = Mathf.Clamp(newScale.x, minSize, maxSize);
    newScale.y = Mathf.Clamp(newScale.y, minSize, maxSize);
    transform.localScale = newScale;
}
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        PlayAudio("die");
        hud.LoseLife();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (right)
        {
        transform.Rotate(0f,0f,-2f);
        rb.AddForce(new Vector2(10f,0f), ForceMode2D.Impulse);
        }else{
        transform.Rotate(0f,0f,2f);
        rb.AddForce(new Vector2(-10f, 0f), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && grounded){
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        animator.SetBool("grounded", false);
        grounded = false;
        }
        return;
    }

    if (Input.GetButtonDown("Jump") && grounded)
    {
        PlayAudio("jump");
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        animator.SetBool("grounded", false);
        grounded = false;
    }
    if(Input.GetKey(KeyCode.Q)){
        PlayAudio("meow");
    }
    if(Input.GetKey(KeyCode.E)){
        PlayAudio("purr");
    }
    if(Input.GetKey(KeyCode.R)){
        SceneManager.LoadScene(currentLevel);
    }
    if(Input.GetKey(KeyCode.M)){
        SceneManager.LoadScene("Main_menu");
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
        if(talking)return;
        if (rolling){
            if(right){
            rb.velocity=new Vector2(speed, rb.velocity.y);
            }else{
            rb.velocity=new Vector2(-speed, rb.velocity.y);
            }
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.CompareTag("ground")){
            animator.SetBool("grounded", true);
            grounded = true;
        }
        if (obj.gameObject.CompareTag("dog")&&!rolling){
           Die();
        }
    }

    IEnumerator escape(){
        yield return new WaitForSeconds(3f);
        bc.enabled = true;
        cc.enabled =false;
        rolling =false;
        animator.SetBool("trapped",false);
        transform.rotation=Quaternion.Euler(0,0,0);
    }
private void OnTriggerEnter2D(Collider2D obj) {
    switch (obj.gameObject.tag) {
        case "dialogueTrigger":
            dialogueTrigger dt = obj.gameObject.GetComponent<dialogueTrigger>();
            if (!dt.spoken) {
                dt.spoken = true;
                triggerDialogue(dt.start, dt.end);
            }
            break;
        case "PositiveCollectable":
            AdjustSize(true);
            Destroy(obj.gameObject); // Destroy the collectable after collection
            break;
        case "NegativeCollectable":
            AdjustSize(false);
            Destroy(obj.gameObject); // Destroy the collectable after collection
            break;
        case "Ryarnball":
        case "Lyarnball":
            right = obj.gameObject.tag == "Ryarnball";
            animator.SetBool("trapped", true);
            rb.velocity = Vector2.zero;
            rolling = true;
            StartCoroutine("escape");
            Destroy(obj.gameObject);
            bc.enabled = false;
            cc.enabled = true;
            break;
        case "door":
            PlayAudio("win");
            break;
        case "key":
            hasKey = true;
            break;
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