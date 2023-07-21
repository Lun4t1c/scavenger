using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotgunScript : WeaponBase
{
    public AudioClip CockingSfx;
    public AudioClip ShellLoadingSfx;

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
        ReloadDuration = .6f;

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

    protected async override void StartReload()
    {
        if (BulletsInMag == MagCapacity || isReloading) return;
        bool hasStartedFromZero = BulletsInMag == 0;
        isReloading = true;

        while (BulletsInMag < MagCapacity)
        {
            await LoadSingleShell();
        }

        if (hasStartedFromZero) Audio.PlayOneShot(CockingSfx, 0.7f);
        isReloading = false;
    }


    private async Task LoadSingleShell()
    {
        EventManager.OnReloadStart?.Invoke(ReloadDuration);
        WeaponPlaceholderScript.OnReloadStart?.Invoke();

        await Task.Delay((int)(ReloadDuration * 1000));
        Audio.PlayOneShot(ShellLoadingSfx, 0.7f);

        TotalAmmo--;
        BulletsInMag++;

        WeaponPlaceholderScript.OnReloadStop?.Invoke();
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
    }
}
