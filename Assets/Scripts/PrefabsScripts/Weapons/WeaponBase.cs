using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected ushort BulletsInMag;
    protected ushort MagCapacity;
    protected ushort ShootCooldown;
    protected ushort TotalAmmo;

    protected virtual void Shoot()
    {
        BulletsInMag--;
    }

    protected void Reload()
    {
        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
    }
}
