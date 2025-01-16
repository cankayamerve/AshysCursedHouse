using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel; // Toggle edilecek panel

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}