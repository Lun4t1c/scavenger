using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    protected int MaxHealth = 8;
    protected int CurrentHealth = 8;
    protected int Damage = 5;
    protected float AttackSpeed = 1.5f;

    protected bool IsDead = false;

    protected NavMeshAgent agent;
    public GameObject targetObject;
    public float targetStoppingDistance = 2f;

    private float timeOfLastAttack = 0;

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

    public virtual void ApplyDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
            Kill();
    }

    protected virtual void Kill()
    {
        IsDead = true;
        Destroy(gameObject);
    }

    protected void MoveToTarget()
    {
        agent.SetDestination(targetObject.transform.position);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(targetObject.transform.position, transform.position);
        if (distanceToTarget <= agent.stoppingDistance) 
        {
            if (Time.time >= timeOfLastAttack + AttackSpeed)
            {
                timeOfLastAttack = Time.time;
                Player player = targetObject.GetComponentInParent<Player>();
                player?.ApplyDamage(Damage);
            }
        }
    }

    protected void RotateToTarget()
    {
        transform.LookAt(targetObject.transform);
    }
}
