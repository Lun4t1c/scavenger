using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected int MaxHealth;
    protected int CurrentHealth;
    protected int Damage;

    protected bool IsDead = false;

    protected NavMeshAgent agent;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void ApplyDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
            Kill();
    }

    protected virtual void Kill()
    {
        IsDead = true;
    }
}
