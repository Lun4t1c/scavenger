using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantScript : EnemyBase
{
    public GameObject projectilePrefab;
    public GameObject Mouth;

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
