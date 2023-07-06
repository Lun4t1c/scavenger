using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float verticalRotationLimit = 80f; // Adjust this value to set the vertical rotation limit
    private float currentRotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseSensitivity = 100f; // Adjust this value to control the mouse sensitivity

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentRotationX -= mouseY;
        currentRotationX = Mathf.Clamp(currentRotationX, -verticalRotationLimit, verticalRotationLimit);

        transform.localRotation = Quaternion.Euler(currentRotationX, transform.localEulerAngles.y + mouseX, 0f);
    }
}
