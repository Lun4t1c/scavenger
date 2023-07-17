using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBase : MonoBehaviour, IDamagable
{
    public int currentHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
            gameObject.SetActive (false);
    }
}
