using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text AmmoText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnAmmoUpdate += UpdateAmmoText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoText(string newText)
    {
        AmmoText.text = newText;
    }
}
