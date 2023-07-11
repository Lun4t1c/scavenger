using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void TextUpdateEvent(string text);
    public delegate void WeaponChangeEvent(WeaponBase weapon);

    public static TextUpdateEvent OnCurrentAmmoUpdate;
    public static TextUpdateEvent OnTotalAmmoUpdate;
    public static WeaponChangeEvent OnWeaponChange;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
