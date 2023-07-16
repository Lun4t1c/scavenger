using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBase : MonoBehaviour
{
    public ushort BulletsInMag;
    public ushort Damage;
    public ushort MagCapacity;
    public ushort ShootCooldown;
    public ushort TotalAmmo;
    public ushort MaxTotalAmmo;
    public float ImpactForce;
    public float FireRate;
    public float Range;
    public float ReloadDuration;

    public Transform GunEnd;

    public AudioSource Audio;
    public AudioClip ShotSfx;
    public AudioClip ReloadSfx;

    private Camera PlayerCamera;
    private WaitForSeconds ShotDuration = new WaitForSeconds(.07f);
    private float NextFire;

    private bool isReloading = false;

    protected void Start()
    {
        GetReferences();
        BulletsInMag = MagCapacity;
    }

    protected void GetReferences()
    {
        Audio = GetComponent<AudioSource>();
        PlayerCamera = GetComponentInParent<Camera>();
    }

    void OnEnable()
    {
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
    }

    protected abstract void Update();

    protected virtual void Shoot()
    {
        if (BulletsInMag > 0
            && Time.time > NextFire
            && !isReloading)
        {
            NextFire = Time.time + FireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit raycastHit;

            if (Physics.Raycast(rayOrigin, PlayerCamera.transform.forward, out raycastHit, Range))
            {
                DestructibleBase destructible = raycastHit.collider.GetComponent<DestructibleBase>();
                destructible?.Damage(Damage);
                raycastHit.rigidbody?.AddForce(-raycastHit.normal * ImpactForce);
            }

            BulletsInMag--;
            NotifyCurrentAmmoUpdate();
        }
    }

    protected void StartReload()
    {
        if (BulletsInMag == MagCapacity || isReloading) return;
        isReloading = true;

        WeaponPlaceholderScript.OnReloadStart?.Invoke();

        Invoke("StopReload", ReloadDuration);   
    }

    protected void StopReload()
    {
        Audio.PlayOneShot(ReloadSfx, 0.7f);
        WeaponPlaceholderScript.OnReloadStop?.Invoke();

        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
        isReloading = false;
    }

    public void AddAmmo(ushort amount)
    {
        if (TotalAmmo == MaxTotalAmmo) return;
        Debug.Log($"Adding - {TotalAmmo} + {amount}");

        TotalAmmo += amount;
        if (TotalAmmo > MaxTotalAmmo) 
            TotalAmmo = MaxTotalAmmo;

        NotifyTotalAmmoUpdate();
    }

    protected IEnumerator ShotEffect()
    {
        Audio.PlayOneShot(ShotSfx, 0.7f);
        yield return ShotDuration;
    }

    protected void NotifyCurrentAmmoUpdate() =>
        EventManager.OnCurrentAmmoUpdate?.Invoke($"{this.BulletsInMag}/{this.MagCapacity}");

    protected void NotifyTotalAmmoUpdate() => 
        EventManager.OnTotalAmmoUpdate?.Invoke(TotalAmmo.ToString());
}
