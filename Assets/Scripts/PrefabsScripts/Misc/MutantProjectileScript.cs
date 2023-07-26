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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Destructible") {
            Destroy(gameObject);
        }
    }
}
