using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject targetObject;

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
        agent.SetDestination(targetObject.transform.position);
    }

    protected void RotateToTarget()
    {
        transform.LookAt(targetObject.transform);
    }
}
