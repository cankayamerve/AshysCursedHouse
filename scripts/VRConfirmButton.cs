using UnityEngine;
using Valve.VR;

public class VRConfirmButton : MonoBehaviour
{
    public DoorManager doorManager;  // Kap� y�neticisi
    public SteamVR_Input_Sources inputSource;  // Kontrol cihaz� t�r� (left veya right controller)
    public SteamVR_Behaviour_Pose controllerPose;  // Kontrol cihaz�n�n pozisyonu
    public SteamVR_Action_Boolean triggerAction;  // Trigger butonunun aksiyonu
    private bool isPressed = false;  // Butonun t�klan�p t�klanmad���n� kontrol eder

    void Start()
    {
        if (doorManager == null)
        {
            Debug.LogError("DoorManager is not assigned in the ConfirmButton.");
        }
    }

    void Update()
    {
        // Trigger'a bas�lma durumunu kontrol et
        if (triggerAction.GetStateDown(inputSource) && !isPressed)
        {
            isPressed = true;
            OnConfirmPressed();
        }
        else if (triggerAction.GetStateUp(inputSource))
        {
            isPressed = false;
        }
    }

    // Confirm butonuna bas�ld���nda tetiklenen fonksiyon
    public void OnConfirmPressed()
    {
        if (doorManager != null)
        {
            doorManager.ConfirmSequence();  // �ifreyi do�rula
            Debug.Log("Confirm button pressed");
        }
    }
}