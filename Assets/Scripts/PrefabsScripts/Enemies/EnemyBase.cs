using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    protected abstract void AttackTarget();

    public AudioSource Audio;
    public AudioClip[] DamagedSfxPool;

    protected int MaxHealth = 8;
    protected int CurrentHealth = 8;
    protected int Damage = 5;
    protected float AttackSpeed = 1.5f;

    protected bool IsDead = false;

    protected NavMeshAgent agent;
    public GameObject targetObject;

    protected float timeOfLastAttack = 0;

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
        Audio = GetComponent<AudioSource>();
    }

    public virtual void ApplyDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
            Kill();
        else 
            Audio.PlayOneShot(DamagedSfxPool[Random.Range(0, DamagedSfxPool.Length - 1)], 0.7f);
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
            AttackTarget();
    }

    protected void RotateToTarget()
    {
        Vector3 targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
