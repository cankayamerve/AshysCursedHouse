using UnityEngine;
using UnityEngine.EventSystems;

public class ShapePad : MonoBehaviour, IPointerClickHandler
{
    public string shape; // ○, Δ, #, ~

    private DoorManager doorManager; // Şekilleri yönetecek sınıf (DoorManager)

    void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();
        if (doorManager == null)
        {
            Debug.LogError("DoorManager not found in scene.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (doorManager != null)
        {
            doorManager.AddSymbol(shape); // Şekli yöneticinin metoduna iletir
            Debug.Log("Shape " + shape + " clicked");
        }
    }
}