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
        FireRate = .1f;
        Range = 150f;
        ReloadDuration = 3;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetMouseButton(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartReload();
    }
}
