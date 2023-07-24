using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantScript : EnemyBase
{
    public GameObject projectilePrefab;
    public GameObject Mouth;

    protected override void Start()
    {
        MaxHealth = 8;
        CurrentHealth = 8;
        Damage = 5;
        AttackSpeed = 3.5f;

        base.Start();
    }

    protected override void AttackTarget()
    {
        if (Time.time >= timeOfLastAttack + AttackSpeed)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, Mouth.transform.position, Mouth.transform.rotation);
            timeOfLastAttack = Time.time;
            //Player player = targetObject.GetComponentInParent<Player>();
            //player?.ApplyDamage(Damage);
        }
    }
}
