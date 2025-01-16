using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.EventSystems;

using Valve.VR;

public class NumberPadForBedroom : MonoBehaviour
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