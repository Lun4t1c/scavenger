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
        ImpactForce = 200f;
        FireRate = .1f;
        Range = 150f;
        ReloadDuration = 1.4f;
        BulletSpreadAngle = 1.5f;

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

    protected override void StartReload()
    {
        if (BulletsInMag == MagCapacity || isReloading) return;

        Audio.PlayOneShot(ReloadSfx, 0.7f);
        EventManager.OnReloadStart?.Invoke(ReloadDuration);
        isReloading = true;
        WeaponPlaceholderScript.OnReloadStart?.Invoke();

        Invoke("StopReload", ReloadDuration);   
    }

    protected override void StopReload()
    {
        WeaponPlaceholderScript.OnReloadStop?.Invoke();

        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
        isReloading = false;
    }
}
