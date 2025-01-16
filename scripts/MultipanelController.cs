using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MultiPanelController : MonoBehaviour
{
    [System.Serializable]
    public class PanelObject
    {
        public GameObject buttonObject; // Paneli açacak nesne (Collider içeren)
        public GameObject panel; // Açýlacak panel
    }

    public List<PanelObject> panelObjects; // Panel-Nesne eþlemesi

    // VR gözlüðü kamerasý
    public Camera vrCamera; // VR kullanýlýrken hangi kamera kullanýlacak

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare týklamasý
        {
            // VR kamera üzerinden bir ray gönder
            Ray ray = vrCamera.ScreenPointToRay(Input.mousePosition); // VR kamerasýný kullanarak ray oluþtur
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Raycast çarpýþma algýlarsa
            {
                foreach (PanelObject panelObject in panelObjects)
                {
                    if (hit.transform.gameObject == panelObject.buttonObject) // Týklanan nesne doðru mu?
                    {
                        TogglePanel(panelObject.panel); // Ýlgili paneli aç/kapat
                        break; // Doðru nesne bulundu, döngüden çýk
                    }
                }
            }
        }
    }

    private void TogglePanel(GameObject panel)
    {
        foreach (PanelObject obj in panelObjects)
        {
            if (obj.panel != null)
            {
                panel.SetActive(true); // Sadece týklanan paneli aç
                Debug.Log("Panel Active: " + panel.activeSelf);

                //obj.panel.SetActive(false); // Tüm panelleri kapat
            }
        }
      //  panel.SetActive(true); // Sadece týklanan paneli aç
    }
}