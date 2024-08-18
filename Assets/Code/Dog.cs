using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    public Transform player;
    // private NavMeshAgent navMeshAgent;

    private void Start()
    {
        // navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player != null)
        {
            // navMeshAgent.SetDestination(player.position);
        }
    }
}
