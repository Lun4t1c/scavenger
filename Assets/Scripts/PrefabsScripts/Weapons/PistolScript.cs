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
        ImpactForce = 150f;
        FireRate = .1f;
        Range = 50f;
        ReloadDuration = 0.5f;
        BulletSpreadAngle = .7f;

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

    protected override void Shoot()
    {
        StartCoroutine(DisplayMuzzleFlashVfx());
        base.Shoot();
    }

    private IEnumerator DisplayMuzzleFlashVfx()
    {
        MuzzleFlashPrefab.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        MuzzleFlashPrefab.gameObject.SetActive(false);
    }
}
