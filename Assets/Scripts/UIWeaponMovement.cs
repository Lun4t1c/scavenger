using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponMovement : MonoBehaviour
{
    public bool shouldMove = false;
    public float amplitude = 1f;
    public float speed = 1f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (shouldMove)
        {
            float z = initialPosition.z + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        else
        {
            // Return to initial position
            transform.position = initialPosition;
        }
    }
}
