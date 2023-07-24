using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotgunScript : WeaponBase
{
    public int PelletsPerBullet;

    public AudioClip CockingSfx;
    public AudioClip ShellLoadingSfx;

    // Start is called before the first frame update
    void Start()
    {
        PelletsPerBullet = 7;

        Damage = 1;
        MagCapacity = 8;
        BulletsInMag = MagCapacity;        
        ShootCooldown = 700;
        TotalAmmo = 60;
        MaxTotalAmmo = 60;
        ImpactForce = 100f;
        FireRate = 1f;
        Range = 100f;
        ReloadDuration = .6f;
        BulletSpreadAngle = 6.0f;

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
            StartCoroutine(DisplayMuzzleFlashVfx());

            Vector3 rayOrigin = PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            
            for (int i = 0; i < PelletsPerBullet; i++)
            {
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
            }

            BulletsInMag--;
            NotifyCurrentAmmoUpdate();
        }
    }

    private IEnumerator DisplayMuzzleFlashVfx()
    {
        MuzzleFlashPrefab.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        MuzzleFlashPrefab.gameObject.SetActive(false);
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
