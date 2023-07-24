using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantProjectileScript : MonoBehaviour
{
    public float speed = 7f; // Speed of the projectile
    public float lifetime = 5f; // Time until the projectile is destroyed if it doesn't hit anything

    private void Start()
    {
        // Destroy the projectile after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with other objects here (e.g., deal damage, apply effects)
        // For example, you could destroy the projectile when it hits something:
        if (collision.gameObject.tag == "Player") {
            //Destroy(gameObject);
        }
    }
}
