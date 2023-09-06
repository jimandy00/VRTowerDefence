using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 요원에게 목적지를 알려주고싶다.
public class Enemy : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
