using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    protected void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
    }
}
