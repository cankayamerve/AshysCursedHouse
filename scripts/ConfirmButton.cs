using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;



public class ConfirmButton : MonoBehaviour, IPointerClickHandler
{
    private PasswordManager passwordManager;

    void Start()
    {
        passwordManager = FindObjectOfType<PasswordManager>();
        if (passwordManager == null)
        {
            Debug.LogError("PasswordManager not found in scene.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (passwordManager != null)
        {
            passwordManager.ConfirmPassword();
            Debug.Log("Confirm button clicked");
        }
    }
}