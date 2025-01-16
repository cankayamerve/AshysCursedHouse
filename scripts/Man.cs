using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Man : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed
    public float moveHeight = 2f; // Movement distance (up and down)
    private Vector3 startPos; // Starting position
    private bool movingUp = true; // Is it moving up?

    void Start()
    {
        startPos = transform.position; 
    }

    void Update()
    {
        //Up and down movement
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= startPos.y + moveHeight)
                movingUp = false; // Move down when the height limit is reached
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= startPos.y)
                movingUp = true; //Move upwards when the starting level is reached
        }
    }
}
