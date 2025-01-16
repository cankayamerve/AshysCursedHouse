using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class NumberButton : MonoBehaviour
{
    public TextMeshPro displayText; // Þifreyi gösterecek TextMeshPro.
    private TextMeshPro textOnCube; // Küpün üzerindeki TextMeshPro.

    private void Start()
    {
        // Küpün üzerindeki TextMeshPro bileþenine eriþiyoruz.
        textOnCube = GetComponentInChildren<TextMeshPro>();
    }

    public void OnButtonPressed()
    {
        if (textOnCube != null)
        {
            // TextMeshPro'dan sayýyý al ve displayText'e ekle.
            displayText.text += textOnCube.text;
        }
        else
        {
            Debug.LogError("TextMeshPro bulunamadý!");
        }
    }
}