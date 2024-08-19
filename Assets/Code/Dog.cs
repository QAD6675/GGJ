using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dog : MonoBehaviour
{
    public Transform catTransform; 
    public float sightRange = 10f; 
    public float chaseSpeed = 5f;  
    public float wanderSpeed = 2f; 
    public float wanderRadius = 5f; 
    public float attackRange = 1.5f; 

    private Vector3 wanderTarget; 
    private bool isChasing = false;

    void Start()
    {
        StartCoroutine(Wander());
    }

    void Update()
    {
        float distanceToCat = Vector3.Distance(transform.position, catTransform.position);

        if (distanceToCat <= sightRange)
        {
            isChasing = true;
            ChaseCat();
        }
        else
        {
            isChasing = false;
        }
    }

    private void ChaseCat()
    {
        transform.position = Vector3.MoveTowards(transform.position, catTransform.position, chaseSpeed * Time.deltaTime);

        
        if (catTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1); 
        else
            transform.localScale = new Vector3(-1, 1, 1); 

        if (Vector3.Distance(transform.position, catTransform.position) <= attackRange)
        {
            AttackCat();
        }
    }

    private void AttackCat()
    {
        //here comes the attack anim
        Debug.Log("Dog is attacking the cat!");
    }

    private IEnumerator Wander()
    {
        while (!isChasing)
        {
            wanderTarget = new Vector3(transform.position.x + Random.Range(-wanderRadius, wanderRadius), transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

         
            if (wanderTarget.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1); 

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

}
