using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MultiPanelController : MonoBehaviour
{
    [System.Serializable]
    public class PanelObject
    {
        public GameObject buttonObject; // Paneli a�acak nesne (Collider i�eren)
        public GameObject panel; // A��lacak panel
    }

    public List<PanelObject> panelObjects; // Panel-Nesne e�lemesi

    // VR g�zl��� kameras�
    public Camera vrCamera; // VR kullan�l�rken hangi kamera kullan�lacak

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare t�klamas�
        {
            // VR kamera �zerinden bir ray g�nder
            Ray ray = vrCamera.ScreenPointToRay(Input.mousePosition); // VR kameras�n� kullanarak ray olu�tur
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Raycast �arp��ma alg�larsa
            {
                foreach (PanelObject panelObject in panelObjects)
                {
                    if (hit.transform.gameObject == panelObject.buttonObject) // T�klanan nesne do�ru mu?
                    {
                        TogglePanel(panelObject.panel); // �lgili paneli a�/kapat
                        break; // Do�ru nesne bulundu, d�ng�den ��k
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
                panel.SetActive(true); // Sadece t�klanan paneli a�
                Debug.Log("Panel Active: " + panel.activeSelf);

                //obj.panel.SetActive(false); // T�m panelleri kapat
            }
        }
      //  panel.SetActive(true); // Sadece t�klanan paneli a�
    }
}