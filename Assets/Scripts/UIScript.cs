using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text CurrentAmmoText;
    public TMP_Text TotalAmmoText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnWeaponChange += WeaponChange;
        EventManager.OnCurrentAmmoUpdate += UpdateCurrentAmmoText;
        EventManager.OnTotalAmmoUpdate += UpdateTotalAmmoText;
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

    public void UpdateCurrentAmmoText(string newText) => CurrentAmmoText.text = newText;
    public void UpdateTotalAmmoText(string newText) => TotalAmmoText.text = newText;
}
