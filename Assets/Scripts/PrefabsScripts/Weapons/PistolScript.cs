using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        BulletsInMag = 15;
        MagCapacity = 15;
        ShootCooldown = 100;
        TotalAmmo = 100;
        MaxTotalAmmo = 100;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
