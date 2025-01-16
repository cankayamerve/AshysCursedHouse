using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    public float moveSpeed = 3f;       // Movement speed
    public float moveDistance = 5f;   // Movement distance
    private Vector3 startPos;         // Starting position
    private bool movingForward = true; // Direction of movement

    void Start()
    {
        startPos = transform.position; 
    }
    void Update()
    {
        // Forward-backward movement
        if (movingForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) >= moveDistance)
                movingForward = false; 
        }
        else
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) <= 0.1f)
                movingForward = true; 
        }
    }

}