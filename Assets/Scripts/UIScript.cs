using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text CurrentAmmoText;
    public TMP_Text TotalAmmoText;
    public TMP_Text HealthText;
    public Image ReloadProgressImage;
    public Image InteractableImage;

    private float _startingProgressarXScale = 0.3f;
    private Coroutine fillCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnWeaponChange += WeaponChange;
        EventManager.OnCurrentAmmoUpdate += UpdateCurrentAmmoText;
        EventManager.OnTotalAmmoUpdate += UpdateTotalAmmoText;
        EventManager.OnReloadStart += StartReload;
        EventManager.OnInteractableFocus += InteractableFocus;
        EventManager.OnInteractableUnfocus += InteractableUnfocus;
        EventManager.OnHealthUpdate += UpdateHealthText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeaponChange(WeaponBase newWeapon)
    {
        UpdateCurrentAmmoText($"{newWeapon.BulletsInMag}/{newWeapon.MagCapacity}");
        UpdateTotalAmmoText(newWeapon.TotalAmmo.ToString());
    }

    public void StartReload(float seconds)
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(FillProgressBar(seconds));
    }

    private IEnumerator FillProgressBar(float seconds)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = new Vector3(0.3f, ReloadProgressImage.transform.localScale.y, ReloadProgressImage.transform.localScale.z);
        Vector3 targetScale = new Vector3(0f, ReloadProgressImage.transform.localScale.y, ReloadProgressImage.transform.localScale.z);

        while (elapsedTime < seconds)
        {
            float t = elapsedTime / seconds;
            ReloadProgressImage.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ReloadProgressImage.transform.localScale = targetScale;
    }

    public void InteractableFocus() => InteractableImage.enabled = true;
    public void InteractableUnfocus() => InteractableImage.enabled = false;

    public void UpdateHealthText(string newText) => HealthText.text = newText;
    public void UpdateCurrentAmmoText(string newText) => CurrentAmmoText.text = newText;
    public void UpdateTotalAmmoText(string newText) => TotalAmmoText.text = newText;    
}
