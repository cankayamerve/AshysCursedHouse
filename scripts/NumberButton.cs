using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class NumberButton : MonoBehaviour
{
    public TextMeshPro displayText; // �ifreyi g�sterecek TextMeshPro.
    private TextMeshPro textOnCube; // K�p�n �zerindeki TextMeshPro.

    private void Start()
    {
        // K�p�n �zerindeki TextMeshPro bile�enine eri�iyoruz.
        textOnCube = GetComponentInChildren<TextMeshPro>();
    }

    public void OnButtonPressed()
    {
        if (textOnCube != null)
        {
            // TextMeshPro'dan say�y� al ve displayText'e ekle.
            displayText.text += textOnCube.text;
        }
        else
        {
            Debug.LogError("TextMeshPro bulunamad�!");
        }
    }
}