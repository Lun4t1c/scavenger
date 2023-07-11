using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBase : MonoBehaviour
{
    protected ushort BulletsInMag;
    protected ushort Damage;
    protected ushort MagCapacity;
    protected ushort ShootCooldown;
    protected ushort TotalAmmo;
    protected ushort MaxTotalAmmo;

    public float FireRate = .25f;
    public float Range = 50f;
    public float ImpactForce = 100f;
    public Transform GunEnd;

    public AudioSource Audio;
    public AudioClip ShotSfx;
    public AudioClip ReloadSfx;

    private Camera PlayerCamera;
    private WaitForSeconds ShotDuration = new WaitForSeconds(.07f);
    private LineRenderer LaserLine;
    private float NextFire;

    protected void Start()
    {
        LaserLine = GetComponent<LineRenderer>();
        Audio = GetComponent<AudioSource>();
        PlayerCamera = GetComponentInParent<Camera>();
        
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
        if (BulletsInMag > 0
            && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit raycastHit;

            LaserLine.SetPosition(0, GunEnd.position);

            if (Physics.Raycast(rayOrigin, PlayerCamera.transform.forward, out raycastHit, Range))
            {
                LaserLine.SetPosition(1, raycastHit.point);

                DestructibleBase destructible = raycastHit.collider.GetComponent<DestructibleBase>();
                destructible?.Damage(Damage);
                raycastHit.rigidbody?.AddForce(-raycastHit.normal * ImpactForce);
            }
            else
            {
                LaserLine.SetPosition(1, rayOrigin + (PlayerCamera.transform.forward * Range));
            }

            BulletsInMag--;
            NotifyAmmoUpdate();
        }
    }

    protected void Reload()
    {
        Audio.PlayOneShot(ReloadSfx, 0.7f);
        TotalAmmo -= (ushort)(MagCapacity - BulletsInMag);
        BulletsInMag = MagCapacity;
        NotifyAmmoUpdate();
    }

    protected void NotifyAmmoUpdate()
    {
        EventManager.OnAmmoUpdate?.Invoke($"{this.BulletsInMag}/{this.MagCapacity}");
    }

    protected IEnumerator ShotEffect()
    {
        Audio.PlayOneShot(ShotSfx, 0.7f);

        LaserLine.enabled = true;
        yield return ShotDuration;
        LaserLine.enabled = false;
    }
}
