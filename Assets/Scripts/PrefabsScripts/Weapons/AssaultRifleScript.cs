using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleScript : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        Damage = 2;
        BulletsInMag = 30;
        MagCapacity = 30;
        ShootCooldown = 100;
        TotalAmmo = 360;
        MaxTotalAmmo = 360;
        ImpactForce = 400f;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
