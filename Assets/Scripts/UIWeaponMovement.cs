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
        
    }
}
