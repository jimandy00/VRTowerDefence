using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �¾ �� ������� �������� �˷��ְ�ʹ�.
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
