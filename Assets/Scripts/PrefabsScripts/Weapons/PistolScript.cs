using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        Damage = 1;
        BulletsInMag = 15;
        MagCapacity = 15;
        ShootCooldown = 100;
        TotalAmmo = 100;
        MaxTotalAmmo = 100;
        ImpactForce = 100f;
        FireRate = .1f;
        Range = 50f;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
