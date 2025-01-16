using UnityEngine;
using Valve.VR;

public class VRConfirmButton : MonoBehaviour
{
    public DoorManager doorManager;  // Kapý yöneticisi
    public SteamVR_Input_Sources inputSource;  // Kontrol cihazý türü (left veya right controller)
    public SteamVR_Behaviour_Pose controllerPose;  // Kontrol cihazýnýn pozisyonu
    public SteamVR_Action_Boolean triggerAction;  // Trigger butonunun aksiyonu
    private bool isPressed = false;  // Butonun týklanýp týklanmadýðýný kontrol eder

    void Start()
    {
        if (doorManager == null)
        {
            Debug.LogError("DoorManager is not assigned in the ConfirmButton.");
        }
    }

    void Update()
    {
        // Trigger'a basýlma durumunu kontrol et
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

    // Confirm butonuna basýldýðýnda tetiklenen fonksiyon
    public void OnConfirmPressed()
    {
        if (doorManager != null)
        {
            doorManager.ConfirmSequence();  // Þifreyi doðrula
            Debug.Log("Confirm button pressed");
        }
    }
}