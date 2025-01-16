using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Boolean moveForwardAction; // Trigger aksiyonu
    public SteamVR_Input_Sources handType; // Hangi el (Sol veya Sað)
    public float moveSpeed = 2.0f; // Hareket hýzý

    private CharacterController characterController;

    void Start()
    {
        // Player prefab'ýnda bir CharacterController olduðundan emin olun
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController bulunamadý! Lütfen Player prefab'ýna ekleyin.");
        }
    }

    void Update()
    {
        if (moveForwardAction.GetState(handType))
        {
            // Trigger'a basýldýðýnda ileri hareket
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // Yukarý/aþaðý hareketi engelle
            forward.Normalize();

            characterController.Move(forward * moveSpeed * Time.deltaTime);
        }
    }
}