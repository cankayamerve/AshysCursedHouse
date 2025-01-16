using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; //Panel reference

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}