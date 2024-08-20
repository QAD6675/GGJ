using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dog : MonoBehaviour
{
    public Transform catTransform; 
    public float sightRange = 10f; 
    public float chaseSpeed = 3f;  
    public float wanderSpeed = 2f; 
    public float wanderRadius = 5f; 
    private Vector3 wanderTarget; 
    public AudioClip[] dogAudio;
    public AudioSource audioSource;
    private Animator animator;
    private bool isChasing = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(6.2f,7f,1);
        animator = GetComponent<Animator>();
        StartCoroutine(Wander());
    }
    public void PlayAudio(string str)
    {
        int i=0;
        if (str=="spot")
        {
            i=Random.Range(0,1);   
        }else if(str=="chase"){
            i=Random.Range(2,dogAudio.Length);
            audioSource.loop=true;
        }
        audioSource.clip = dogAudio[i];
        audioSource.Play();
    }

    void Update()
    {
        float distanceToCat = Vector3.Distance(transform.position, catTransform.position);

        if (distanceToCat <= sightRange)
        {
            animator.SetBool("spottedPlayer",true);
            animator.SetBool("wandering",false);
            PlayAudio("spot");
            isChasing = true;
            StartCoroutine(waitForAnimation());
        }
        else
        {
            isChasing = false;
        }
    }
    IEnumerator waitForAnimation(){
        yield return new WaitForSeconds(1f);
        animator.SetBool("spottedPlayer",false);
        animator.SetBool("chasingPlayer",true);
        ChaseCat();
        PlayAudio("chase");
    }

    private void ChaseCat()
    {
        transform.position = Vector3.MoveTowards(transform.position, catTransform.position, chaseSpeed * Time.deltaTime);
        
        if (catTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(6.2f,7f,1); 
        else
            transform.localScale = new Vector3(-6.2f,7f,1); 

    }
    private IEnumerator Wander()
    {
        animator.SetBool("wandering",true);
        while (!isChasing)
        {
            wanderTarget = new Vector3(transform.position.x + Random.Range(-wanderRadius, wanderRadius), transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);
         
            if (wanderTarget.x > transform.position.x)
                transform.localScale = new Vector3(8.2f,9f,1);
            else
                transform.localScale = new Vector3(-8.2f,9f,1); 

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

}
