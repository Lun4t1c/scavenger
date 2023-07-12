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

    public Transform GunEnd;

    public AudioSource Audio;
    public AudioClip ShotSfx;
    public AudioClip ReloadSfx;

    private Camera PlayerCamera;
    private WaitForSeconds ShotDuration = new WaitForSeconds(.07f);
    private float NextFire;

    protected void Start()
    {
        Audio = GetComponent<AudioSource>();
        PlayerCamera = GetComponentInParent<Camera>();
        
        BulletsInMag = MagCapacity;
    }

    void OnEnable()
    {
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
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
        if (BulletsInMag > 0
            && Time.time > NextFire)
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

    protected void Reload()
    {
        Audio.PlayOneShot(ReloadSfx, 0.7f);
        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
        NotifyCurrentAmmoUpdate();
        NotifyTotalAmmoUpdate();
    }

    protected void NotifyCurrentAmmoUpdate()
    {
        EventManager.OnCurrentAmmoUpdate?.Invoke($"{this.BulletsInMag}/{this.MagCapacity}");
    }

    protected void NotifyTotalAmmoUpdate()
    {
        EventManager.OnTotalAmmoUpdate?.Invoke(TotalAmmo.ToString());
    }

    protected IEnumerator ShotEffect()
    {
        Audio.PlayOneShot(ShotSfx, 0.7f);

        yield return ShotDuration;
    }
}
