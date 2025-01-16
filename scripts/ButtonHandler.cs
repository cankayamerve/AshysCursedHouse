using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    //The player can only click on the panels when close to them, preventing the panels from opening from a long distance.
    //If the panel is open and the player moves away, it closes after a certain distance.

    public GameObject panel;
    public Camera playerCamera;

    public float interactionDistance = 6f;
    public float maxDistance = 10f;

    //It checks the distance in every frame
    void Update()
    {
        float distance = Vector3.Distance(playerCamera.transform.position, transform.position); //Calculating the distance between the camera and the button.

        if (distance <= interactionDistance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Checking whether the mouse clicked on the button.
                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform) 
                    {
                        if (panel != null && !panel.activeSelf) 
                        {
                            panel.SetActive(true);
                        }
                    }
                }
            }
        }
        if (panel.activeSelf && distance > maxDistance)
        {
            panel.SetActive(false); 
        }
    }
}
