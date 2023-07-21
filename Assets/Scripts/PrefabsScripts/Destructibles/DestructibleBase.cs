using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBase : MonoBehaviour, IDamagable
{
    public int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void ApplyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
            DestroyDesctructible();
    }

    protected virtual void DestroyDesctructible()
    {
        Destroy(gameObject);
    }
}
