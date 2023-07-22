using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EnemyBase
{
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
