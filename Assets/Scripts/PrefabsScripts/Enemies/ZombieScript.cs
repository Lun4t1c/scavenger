using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EnemyBase
{
    protected override void Start()
    {
        MaxHealth = 8;
        CurrentHealth = 8;
        Damage = 5;
        AttackSpeed = 1.5f;

        base.Start();
    }

    protected override void AttackTarget()
    {
        if (Time.time >= timeOfLastAttack + AttackSpeed)
        {
            timeOfLastAttack = Time.time;
            Player player = targetObject.GetComponentInParent<Player>();
            player?.ApplyDamage(Damage);
        }
    }
}
