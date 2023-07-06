using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Adjust this value to control the mouse sensitivity
    private float rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 5f; // Adjust this value to control the movement speed        
    
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Mouse rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
