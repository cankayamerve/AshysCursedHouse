using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;


public class DoorManager : MonoBehaviour
{
    [Header("Password Settings")]
    public List<string> correctSequence = new List<string>() { "○", "Δ", "#", "~" }; // Doğru sıradaki şekiller
    private List<string> enteredSequence = new List<string>(); // Girilen şekiller

    [Header("Door Settings")]
    public Transform door; // Kapının Transform'u
    public float openDuration = 2f; // Kapının açılma süresi

    [Header("UI Settings")]
    public TMPro.TextMeshPro feedbackText; // Girilen şekiller ve geri bildirim için UI

    private bool isUnlocked = false;

    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door Transform is not assigned.");
        }

        feedbackText.text = ""; // Başlangıçta text boş olacak
    }

    public void AddSymbol(string shape)
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        // Girilen şekil sayısı doğru değilse hata mesajı gösterilecek
        if (enteredSequence.Count >= correctSequence.Count)
        {
            Debug.Log("Maximum sequence length reached. Clearing sequence.");
            ClearSequence();
        }

        enteredSequence.Add(shape);  // Şekil ekleniyor
        Debug.Log("Added shape: " + shape);
        UpdateFeedbackText();

        // Şekil sayısı doğru uzunluktaysa kontrol et
        if (enteredSequence.Count == correctSequence.Count)
        {
            CheckSequence();
        }
    }

    private void CheckSequence()
    {

        // Girilen şekil sırasını doğru mu kontrol et
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (enteredSequence[i] != correctSequence[i])
            {
                Debug.Log("Incorrect sequence!");
                StartCoroutine(ShowFeedback(false));  // Hatalı sıralama
                ClearSequence();
                return;
            }
        }

        Debug.Log("Correct sequence!");
        StartCoroutine(ShowFeedback(true));  // Doğru sıralama
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        Quaternion targetRotation = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y + 90, door.eulerAngles.z); // Kapıyı 90 derece aç
        StartCoroutine(OpenDoor(door, targetRotation, openDuration));
    }

    private IEnumerator OpenDoor(Transform door, Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = door.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            door.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.rotation = targetRotation; // Kapıyı tamamen aç
    }

    private IEnumerator ShowFeedback(bool isCorrect)
    {
        yield return new WaitForSeconds(1.5f);
        if (isCorrect)
        {
            feedbackText.text = "<color=green>SUCCESS!!</color>"; // Başarı mesajı
        }
        else
        {
            feedbackText.text = "<color=red>INVALID!!</color>"; // Hatalı mesaj
        }

        yield return new WaitForSeconds(1.5f); // Geri bildirimi 1.5 saniye göster
        UpdateFeedbackText();
    }

    private void ClearSequence()
    {
        enteredSequence.Clear();
        UpdateFeedbackText();
    }

    private void UpdateFeedbackText()
    {
        string display = "";
        foreach (string shape in enteredSequence)
        {
            display += shape + " ";
        }

        feedbackText.text = display.Trim(); // Şekilleri UI'da göster
    }

    public void ConfirmSequence()
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        // Girilen şekil sayısı doğru değilse hata mesajı
        if (enteredSequence.Count != correctSequence.Count)
        {
           // feedbackText.text = "<color=red>INVALID!!</color>";

            return;
        }

        // Şifreyi kontrol et
        CheckSequence();
    }
}
/*
public class DoorManager : MonoBehaviour
{
    [Header("Password Settings")]
    public List<string> correctSequence = new List<string>() { "○", "Δ", "#", "~" }; // Doğru sıradaki şekiller
    private List<string> enteredSequence = new List<string>(); // Girilen şekiller

    [Header("Door Settings")]
    public Transform door; // Kapının Transform'u
    public float openDuration = 2f; // Kapının açılma süresi

    [Header("UI Settings")]
    public TMPro.TextMeshPro feedbackText; // Girilen şekiller ve geri bildirim için UI

    private bool isUnlocked = false;

    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door Transform is not assigned.");
        }

        feedbackText.text = ""; // Başlangıçta text boş olacak
    }

    public void AddSymbol(string shape)
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        if (enteredSequence.Count >= correctSequence.Count)
        {
            Debug.Log("Maximum sequence length reached. Clearing sequence.");
            ClearSequence();
        }

        enteredSequence.Add(shape);
        Debug.Log("Added shape: " + shape);
        UpdateFeedbackText();

        // Şekil sayısı doğru uzunluktaysa kontrol et
        if (enteredSequence.Count == correctSequence.Count)
        {
            CheckSequence();
        }
    }

    private void CheckSequence()
    {
        // Girilen şekil sırasını doğru mu kontrol et
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (enteredSequence[i] != correctSequence[i])
            {
                Debug.Log("Incorrect sequence!");
                StartCoroutine(ShowFeedback(false));  // Hatalı sıralama
                ClearSequence();
                return;
            }
        }

        Debug.Log("Correct sequence!");
        StartCoroutine(ShowFeedback(true));  // Doğru sıralama
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        Quaternion targetRotation = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y + 90, door.eulerAngles.z); // Kapıyı 90 derece aç
        StartCoroutine(OpenDoor(door, targetRotation, openDuration));
    }

    private IEnumerator OpenDoor(Transform door, Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = door.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            door.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.rotation = targetRotation; // Kapıyı tamamen aç
    }

    private IEnumerator ShowFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            feedbackText.text = "<color=green>ACCESS GRANTED!</color>"; // Başarı mesajı
        }
        else
        {
            feedbackText.text = "<color=red>INCORRECT SEQUENCE!</color>"; // Hatalı mesaj
        }

        yield return new WaitForSeconds(1.5f); // Geri bildirimi 1.5 saniye göster
        UpdateFeedbackText();
    }

    private void ClearSequence()
    {
        enteredSequence.Clear();
        UpdateFeedbackText();
    }

    private void UpdateFeedbackText()
    {
        string display = "";
        foreach (string shape in enteredSequence)
        {
            display += shape + " ";
        }

        feedbackText.text = display.Trim(); // Şekilleri UI'da göster
    }

    public void ConfirmSequence()
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        // Girilen şekil sayısı doğru değilse hata mesajı
        if (enteredSequence.Count != correctSequence.Count)
        {
            feedbackText.text = "<color=yellow>Incomplete sequence! Enter all shapes.</color>";
            return;
        }

        // Şifreyi kontrol et
        CheckSequence();
    }
}



/*
public class DoorManager : MonoBehaviour
{
    [Header("Password Settings")]
    public List<string> correctSequence = new List<string>() { "○", "Δ", "#", "~" }; // Doğru sıradaki şekiller
    private List<string> enteredSequence = new List<string>(); // Girilen şekiller

    [Header("Door Settings")]
    public Transform door; // Kapının Transform'u
    public float openDuration = 2f; // Kapının açılma süresi

    [Header("UI Settings")]
    public TMPro.TextMeshPro feedbackText; // Girilen şekiller ve geri bildirim için UI

    private bool isUnlocked = false;

    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door Transform is not assigned.");
        }

        UpdateFeedbackText();
    }

    public void AddSymbol(string shape)
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        if (enteredSequence.Count >= correctSequence.Count)
        {
            Debug.Log("Maximum sequence length reached. Clearing sequence.");
            ClearSequence();
        }

        enteredSequence.Add(shape);
        Debug.Log("Added shape: " + shape);
        UpdateFeedbackText();

        // Şekil sayısı doğru uzunluktaysa kontrol et
        if (enteredSequence.Count == correctSequence.Count)
        {
            CheckSequence();
        }
    }

    private void CheckSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (enteredSequence[i] != correctSequence[i])
            {
                Debug.Log("Incorrect sequence!");
                StartCoroutine(ShowFeedback(false));
                ClearSequence();
                return;
            }
        }

        Debug.Log("Correct sequence!");
        StartCoroutine(ShowFeedback(true));
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        Quaternion targetRotation = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y + 90, door.eulerAngles.z); // Kapıyı 90 derece aç
        StartCoroutine(OpenDoor(door, targetRotation, openDuration));
    }

    private IEnumerator OpenDoor(Transform door, Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = door.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            door.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.rotation = targetRotation; // Kapıyı tamamen aç
    }

    private IEnumerator ShowFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            feedbackText.text = "<color=green>ACCESS GRANTED!</color>";
        }
        else
        {
            feedbackText.text = "<color=red>INCORRECT SEQUENCE!</color>";
        }

        yield return new WaitForSeconds(1.5f); // Geri bildirimi 1.5 saniye göster
        UpdateFeedbackText();
    }

    private void ClearSequence()
    {
        enteredSequence.Clear();
        UpdateFeedbackText();
    }

    private void UpdateFeedbackText()
    {
        string display = "";
        foreach (string shape in enteredSequence)
        {
            display += shape + " ";
        }

        feedbackText.text = display.Trim(); // Şekilleri UI'da göster
    }

    public void ConfirmSequence()
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        // Girilen şekil sayısı doğru değilse hata mesajı
        if (enteredSequence.Count != correctSequence.Count)
        {
            feedbackText.text = "<color=yellow>Incomplete sequence! Enter all shapes.</color>";
            return;
        }

        // Şifreyi kontrol et
        CheckSequence();
    }
}*/