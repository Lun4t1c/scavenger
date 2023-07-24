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
    public float BulletSpreadAngle;

    public Transform GunEnd;
    public GameObject BulletHolePrefab;
    public GameObject MuzzleFlashPrefab;

    public AudioSource Audio;
    public AudioClip ShotSfx;
    public AudioClip ReloadSfx;

    protected Camera PlayerCamera;
    protected WaitForSeconds ShotDuration = new WaitForSeconds(.07f);
    protected float NextFire;

    protected bool isReloading = false;

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
        if (BulletsInMag == 0)
        {
            StartReload();
            return;
        }

        if (BulletsInMag > 0
            && Time.time > NextFire
            && !isReloading)
        {
            NextFire = Time.time + FireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            Vector3 spreadDirection = PlayerCamera.transform.forward;
            spreadDirection = Quaternion.AngleAxis(Random.Range(-BulletSpreadAngle, BulletSpreadAngle), PlayerCamera.transform.up) * spreadDirection;
            spreadDirection = Quaternion.AngleAxis(Random.Range(-BulletSpreadAngle, BulletSpreadAngle), PlayerCamera.transform.right) * spreadDirection;
            RaycastHit raycastHit;

            if (Physics.Raycast(rayOrigin, spreadDirection, out raycastHit, Range))
            {
                IDamagable damagable = raycastHit.collider.GetComponent<IDamagable>();
                damagable?.ApplyDamage(Damage);
                raycastHit.rigidbody?.AddForce(-raycastHit.normal * ImpactForce);

                if (damagable == null)
                {
                    float offset = 0.01f; // Tweak this value as needed
                    Vector3 spawnPosition = raycastHit.point + raycastHit.normal * offset;
                    
                    // Create the image GameObject
                    GameObject imageGO = Instantiate(BulletHolePrefab, spawnPosition, Quaternion.identity);

                    // Calculate the rotation to align the bullet hole with the hit surface
                    Quaternion rotation = Quaternion.LookRotation(raycastHit.normal, Vector3.up);

                    // Apply the rotation to the image GameObject
                    imageGO.transform.rotation = rotation;
                }
            }

            BulletsInMag--;
            NotifyCurrentAmmoUpdate();
        }
    }

    protected virtual void StartReload()
    {
        if (BulletsInMag == MagCapacity || isReloading) return;

        EventManager.OnReloadStart?.Invoke(ReloadDuration);
        isReloading = true;
        WeaponPlaceholderScript.OnReloadStart?.Invoke();

        Invoke("StopReload", ReloadDuration);
    }

    protected virtual void StopReload()
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

        TotalAmmo += amount;
        if (TotalAmmo > MaxTotalAmmo) 
            TotalAmmo = MaxTotalAmmo;

        if (gameObject.active) NotifyTotalAmmoUpdate();
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
