using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float rotationSpeed = 100f; 

    private float currentYRotation = 0f; // The rotation angle to the right or left

    void Start()
    {
        currentYRotation = transform.eulerAngles.y; // The initial rotation on the Y axis (for camera)
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // Movement for W and S
        if (moveZ != 0)
        {
            transform.Translate(0, 0, moveZ); //Forward and backward movement
        }

        // Right and left rotation (A and D keys)
        float turnDirection = Input.GetAxis("Horizontal"); 
        if (turnDirection != 0)
        {
            //Changing the angle over time and enabling rotation around the Y axis
            currentYRotation += turnDirection * rotationSpeed * Time.deltaTime; 
            transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        }
    }
}
