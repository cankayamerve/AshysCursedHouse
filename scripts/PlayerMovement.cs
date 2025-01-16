using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Boolean moveForwardAction; // Trigger aksiyonu
    public SteamVR_Input_Sources handType; // Hangi el (Sol veya Sa�)
    public float moveSpeed = 2.0f; // Hareket h�z�

    private CharacterController characterController;

    void Start()
    {
        // Player prefab'�nda bir CharacterController oldu�undan emin olun
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController bulunamad�! L�tfen Player prefab'�na ekleyin.");
        }
    }

    void Update()
    {
        if (moveForwardAction.GetState(handType))
        {
            // Trigger'a bas�ld���nda ileri hareket
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // Yukar�/a�a�� hareketi engelle
            forward.Normalize();

            characterController.Move(forward * moveSpeed * Time.deltaTime);
        }
    }
}