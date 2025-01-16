using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections;

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PasswordManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshPro passwordAndFeedbackDisplay;

    [Header("Password Settings")]
    // Şifre ve sembol eşleştirmesi için bir Dictionary kullanıyoruz
    public Dictionary<string, string> passwordSymbols = new Dictionary<string, string>()
    {
        { "5247", "○" }, // Daire
        { "6941", "Δ" }, // Üçgen
        { "4381", "#" },  // Hash
        { "3682", "~" }   // Tilde
    };

    private string enteredPassword = "";
    private bool isLocked = false;

    [Header("Feedback Settings")]
    public float flashDuration = 0.5f;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    private Color originalColor;

    [Header("Number Pads")]
    public GameObject[] numberPads; // Inspector'de tüm sayı küplerini atayın

    void Start()
    {
        if (passwordAndFeedbackDisplay == null)
        {
            Debug.LogError("PasswordAndFeedbackDisplay is not assigned.");
            return;
        }

        originalColor = passwordAndFeedbackDisplay.color;
        UpdatePasswordDisplay();
        Debug.Log("PasswordManager initialized");
    }

    public void AddNumber(string number)
    {
        if (isLocked)
        {
            Debug.Log("Input is locked");
            return;
        }
        if (enteredPassword.Length >= 4)
        {
            Debug.Log("Password length limit reached");
            return;
        }

        enteredPassword += number;
        Debug.Log("Number added: " + number + " | Entered Password: " + enteredPassword);
        UpdatePasswordDisplay();
    }

    public void ClearPassword()
    {
        enteredPassword = "";
        Debug.Log("Password cleared");
        UpdatePasswordDisplay();
    }

    public void ConfirmPassword()
    {
        Debug.Log("Confirming password: " + enteredPassword);
        if (passwordSymbols.ContainsKey(enteredPassword))
        {
            // Doğru şifre ve sembol
            string symbol = passwordSymbols[enteredPassword];
            passwordAndFeedbackDisplay.text = enteredPassword + " " + symbol;
            passwordAndFeedbackDisplay.color = correctColor;
            Debug.Log("Password correct with symbol: " + symbol);

            // Bir süre bekledikten sonra sıfırla
            StartCoroutine(ResetAfterSuccess());
        }
        else
        {
            // Yanlış şifre
            Debug.Log("Password incorrect");
            StartCoroutine(FlashIncorrect());
        }
    }

    private IEnumerator ResetAfterSuccess()
    {
        // Doğru sonucu bir süre göster
        yield return new WaitForSeconds(2f);

        // Şifreyi sıfırla ve geri dön
        ClearPassword();
        passwordAndFeedbackDisplay.color = originalColor;
        Debug.Log("System reset, ready for new input.");
    }

    private IEnumerator FlashIncorrect()
    {
        passwordAndFeedbackDisplay.color = incorrectColor;
        yield return new WaitForSeconds(flashDuration);
        passwordAndFeedbackDisplay.color = originalColor;
        ClearPassword();
    }

    private void UpdatePasswordDisplay()
    {
        passwordAndFeedbackDisplay.text = enteredPassword;
    }
}
/*
public class PasswordManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshPro passwordAndFeedbackDisplay;

    [Header("Password Settings")]
    // Şifre ve sembol eşleştirmesi için bir Dictionary kullanıyoruz
    public Dictionary<string, string> passwordSymbols = new Dictionary<string, string>()
    {
        { "5247", "○" }, // Daire
        { "6941", "Δ" }, // Üçgen
        { "4381", "#" },  // Hash
        { "3682", "~" }   // Tilde
    };

    private string enteredPassword = "";
    private bool isLocked = false;

    [Header("Feedback Settings")]
    public float flashDuration = 0.5f;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    private Color originalColor;

    [Header("Number Pads")]
    public GameObject[] numberPads; // Inspector'de tüm sayı küplerini atayın

    void Start()
    {
        if (passwordAndFeedbackDisplay == null)
        {
            Debug.LogError("PasswordAndFeedbackDisplay is not assigned.");
            return;
        }

        originalColor = passwordAndFeedbackDisplay.color;
        UpdatePasswordDisplay();
        Debug.Log("PasswordManager initialized");
    }

    public void AddNumber(string number)
    {
        if (isLocked)
        {
            Debug.Log("Input is locked");
            return;
        }
        if (enteredPassword.Length >= 4)
        {
            Debug.Log("Password length limit reached");
            return;
        }

        enteredPassword += number;
        Debug.Log("Number added: " + number + " | Entered Password: " + enteredPassword);
        UpdatePasswordDisplay();
    }

    public void ClearPassword()
    {
        enteredPassword = "";
        Debug.Log("Password cleared");
        UpdatePasswordDisplay();
    }

    public void ConfirmPassword()
    {
     /*   if (isLocked)
        {
            Debug.Log("Password input is locked");
            return;
        }
     *//*
        Debug.Log("Confirming password: " + enteredPassword);
        if (passwordSymbols.ContainsKey(enteredPassword))
        {
            // Doğru şifre ve sembol
            string symbol = passwordSymbols[enteredPassword];
            passwordAndFeedbackDisplay.text = enteredPassword + " " + symbol;
            passwordAndFeedbackDisplay.color = correctColor;
            Debug.Log("Password correct with symbol: " + symbol);

            // Giriş kilitle
            // isLocked = true; // Eğer kilitlemek isterseniz yorum satırını kaldırın

            // Tüm sayı küplerini devre dışı bırak
            /*   foreach (GameObject pad in numberPads)
               {
                   pad.SetActive(false);
                   Debug.Log("Deactivated pad: " + pad.name);
               }*/

/*       }
       else
       {
           // Yanlış şifre
           Debug.Log("Password incorrect");
           StartCoroutine(FlashIncorrect());
       }
   }

   private IEnumerator FlashIncorrect()
   {
       passwordAndFeedbackDisplay.color = incorrectColor;
       yield return new WaitForSeconds(flashDuration);
       passwordAndFeedbackDisplay.color = originalColor;
       ClearPassword();
   }

   private void UpdatePasswordDisplay()
   {
       passwordAndFeedbackDisplay.text = enteredPassword;
   }
}
*/
/*

public class PasswordManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshPro passwordAndFeedbackDisplay;

    [Header("Password Settings")]
    public string realPassword = "4381";
    private string enteredPassword = "";
    private bool isLocked = false;

    [Header("Feedback Settings")]
    public float flashDuration = 0.5f;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    private Color originalColor;

    [Header("Number Pads")]
    public GameObject[] numberPads; // Inspector'de tüm sayı küplerini atayın

    void Start()
    {
        if (passwordAndFeedbackDisplay == null)
        {
            Debug.LogError("PasswordAndFeedbackDisplay is not assigned.");
            return;
        }

        originalColor = passwordAndFeedbackDisplay.color;
        UpdatePasswordDisplay();
        Debug.Log("PasswordManager initialized");
    }

    public void AddNumber(string number)
    {
         if (isLocked)
         {
             Debug.Log("Input is locked");
             return;
         }
        if (enteredPassword.Length >= 4)
        {
            Debug.Log("Password length limit reached");
            return;
        }

        enteredPassword += number;
        Debug.Log("Number added: " + number + " | Entered Password: " + enteredPassword);
        UpdatePasswordDisplay();
    }

    public void ClearPassword()
    {
        enteredPassword = "";
        Debug.Log("Password cleared");
        UpdatePasswordDisplay();
    }

    public void ConfirmPassword()
    {
          if (isLocked)
          {
              Debug.Log("Password input is locked");
              return;
          }

        Debug.Log("Confirming password: " + enteredPassword);
        if (enteredPassword == realPassword)
        {
            // Doğru şifre

            passwordAndFeedbackDisplay.text = enteredPassword + "#";
            passwordAndFeedbackDisplay.color = correctColor;
            Debug.Log("Password correct");

            // Giriş kilitle
            //  isLocked = true;

            // Tüm sayı küplerini devre dışı bırak
              foreach (GameObject pad in numberPads)
              {
                  pad.SetActive(false);
                  Debug.Log("Deactivated pad: " + pad.name);
              }
        }
        else
        {
            // Yanlış şifre
            Debug.Log("Password incorrect");
            StartCoroutine(FlashIncorrect());
        }
    }

    private IEnumerator FlashIncorrect()
    {
        passwordAndFeedbackDisplay.color = incorrectColor;
        yield return new WaitForSeconds(flashDuration);
        passwordAndFeedbackDisplay.color = originalColor;
        ClearPassword();
    }

    private void UpdatePasswordDisplay()
    {
        passwordAndFeedbackDisplay.text = enteredPassword;
    }
}
*/