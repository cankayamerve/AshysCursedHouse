using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerVR : MonoBehaviour
{
    // The panel to be toggled
    public GameObject panel;

    // The VR camera rig or player's head (used to calculate distance)
    public Transform vrHead;

    public float interactionDistance = 6f; // Distance within which interaction is allowed
    public float maxDistance = 10f; // Distance beyond which the panel closes

    // Optional: Highlight the button or show a visual indicator
    public Renderer buttonRenderer;
    public Color highlightColor = Color.green; // Color when within interaction range
    private Color originalColor;

    void Start()
    {
        // Store the original color of the button (if applicable)
        if (buttonRenderer != null)
        {
            originalColor = buttonRenderer.material.color;
        }
    }

    void Update()
    {
        // Calculate the distance between the VR headset and the button
        float distance = Vector3.Distance(vrHead.position, transform.position);

        // Highlight the button when within interaction range
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = (distance <= interactionDistance) ? highlightColor : originalColor;
        }

        // Open the panel automatically when within interaction distance
        if (distance <= interactionDistance && panel != null && !panel.activeSelf)
        {
            
            panel.SetActive(true); // Open the panel
            Debug.Log("Panel Active: " + panel.activeSelf);
            Debug.Log("Panel Position: " + panel.transform.position);
            Debug.Log("Distance to Panel: " + Vector3.Distance(vrHead.position, panel.transform.position));
        }

        // Automatically close the panel if the player moves too far away
        if (panel.activeSelf && distance > maxDistance)
        {
            panel.SetActive(false);
        }
    }
}