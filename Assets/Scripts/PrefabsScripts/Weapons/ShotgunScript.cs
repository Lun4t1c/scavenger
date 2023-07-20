using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        Damage = 2;
        MagCapacity = 8;
        BulletsInMag = MagCapacity;        
        ShootCooldown = 700;
        TotalAmmo = 60;
        MaxTotalAmmo = 60;
        ImpactForce = 400f;
        FireRate = 1f;
        Range = 100f;
        ReloadDuration = .4f;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartReload();
    }
}
