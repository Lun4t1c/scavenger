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

    public UnityEvent AmmoUpdate;

    protected virtual void Shoot()
    {
        BulletsInMag--;
        AmmoUpdate.Invoke();
    }

    protected void Reload()
    {
        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
    }
}
