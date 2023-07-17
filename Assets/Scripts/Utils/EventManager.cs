using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void GenericGameEvent();
    public delegate void TextUpdateEvent(string text);
    public delegate void WeaponChangeEvent(WeaponBase weapon);
    public delegate void TimedEvent(float seconds);

    public static TextUpdateEvent OnHealthUpdate;
    public static TextUpdateEvent OnCurrentAmmoUpdate;
    public static TextUpdateEvent OnTotalAmmoUpdate;
    public static WeaponChangeEvent OnWeaponChange;
    public static TimedEvent OnReloadStart;
    public static GenericGameEvent OnInteractableFocus;
    public static GenericGameEvent OnInteractableUnfocus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
