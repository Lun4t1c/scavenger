using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EnemyBase
{
    void Start()
    {
        GetReferences();
    }

    void Update()
    {
        MoveToTarget();
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
