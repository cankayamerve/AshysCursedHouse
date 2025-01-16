using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CodeEntryPanel : MonoBehaviour
{
    public TextMeshProUGUI enteredCodeText; // TextMeshPro object that will display both the password and messages
    private string enteredCode = ""; // Variable to store the entered password
    private bool isCodeCorrect = false; // Variable to store whether the password is correct or not
    private Color originalTextColor; 
    public string correctCode = "0000"; // A place to store the passwords of the panels (To be set for each panel)
    public string key = "-"; // Password parts to be collected for the final door
    public Button confirmButton; // Reference to the confirm button
    public Transform door; // Reference to the door
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script

    void Start()
    {
        originalTextColor = Color.black; 
        enteredCodeText.color = originalTextColor;
        enteredCodeText.fontSize = 24f; 
        enteredCodeText.alignment = TextAlignmentOptions.Center; 
        enteredCodeText.fontStyle = FontStyles.Bold; 
    }

    public void OnNumberButtonClicked(string number)
    {
        if (!isCodeCorrect && enteredCode.Length < 4)
        {
            enteredCode += number; 
            enteredCodeText.text = "<color=#000000>" + enteredCode + "</color>"; 

            if (enteredCodeText.color != Color.red)
            {
                enteredCodeText.color = Color.black; 
            }
        }
    }

    public void OnConfirmButtonClicked()
    {
        if (enteredCode == correctCode)
        {
            enteredCodeText.text = "<color=#00FF00>" + key + "</color>"; // Make the text green if it's correct
            isCodeCorrect = true; 
            enteredCode = "";

            confirmButton.interactable = false; //Disable the confirm button (make it unclickable).

            //If the exit door's (the last door) password is entered correctly, the door will slowly open, and a message indicating that the game is won will be displayed.
            if (key == "SUCCESS") 
            {
                Quaternion targetRotation = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y + 90, door.eulerAngles.z);
                StartCoroutine(OpenDoor(door, targetRotation, 2f)); 

                countdownTimer.Win();
            }

        }
        else
        {
            enteredCodeText.text = "<color=#FF0000>INVALID!</color>"; // If the password is incorrect, provide an instant warning in red 
            enteredCode = ""; 
            Invoke("ClearStatusText", 0.5f); 
        }
    }

    private IEnumerator OpenDoor(Transform door, Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = door.rotation; //The current rotation of the door
        float elapsedTime = 0f; 

        while (elapsedTime < duration)
        {
            door.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration); //Smoothly change the rotation
            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        door.rotation = targetRotation; // Set it to the final rotation
    }

    private void ClearStatusText()
    {
        enteredCodeText.text = ""; 
        enteredCodeText.color = originalTextColor; 
    }
    public void OnExitButtonClicked()
    {
        this.gameObject.SetActive(false); //Close the panel
    }

}
