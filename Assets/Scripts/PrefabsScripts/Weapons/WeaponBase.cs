using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBase : MonoBehaviour
{
    protected ushort BulletsInMag;
    protected ushort MagCapacity;
    protected ushort ShootCooldown;
    protected ushort TotalAmmo;
    protected ushort MaxTotalAmmo;

    protected void Start()
    {
        BulletsInMag = MagCapacity;
        NotifyAmmoUpdate();
    }

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    protected virtual void Shoot()
    {
        if (BulletsInMag > 0)
        {
            BulletsInMag--;
            NotifyAmmoUpdate();
        }
    }

    protected void Reload()
    {
        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
        NotifyAmmoUpdate();
    }

    protected void NotifyAmmoUpdate()
    {
        EventManager.OnAmmoUpdate?.Invoke($"{this.BulletsInMag}/{this.MagCapacity}");
    }
}
