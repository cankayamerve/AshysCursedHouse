using UnityEngine;
using UnityEngine.EventSystems;

using Valve.VR;

public class NumberPad : MonoBehaviour, IPointerClickHandler
{
    public string number; // 0-9

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
            passwordManager.AddNumber(number);
            Debug.Log("Number " + number + " clicked");
        }
    }
}