using UnityEngine;
using TMPro;
using System.Collections;
using TMPro.Examples;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // TextMeshPro object that will display the timer
    public float totalTime = 2400f; // Total time (40 minutes)
    private float timeRemaining; // Remaining time
    private bool isTimerRunning = true; // Is the timer running?
    private bool isWarningActive = false; // Checks if the warning is active
    public GameObject gameOverPanel; // Panel to be shown when the game is over
    public GameObject winnerPanel; // Panel to be shown when the player wins
    public CameraController cameraController; // Reference to the script that controls the camera's movement
    private Light directionalLight; // Reference to the Directional Light in the scene

   // public PasswordManager passwordManager; // Eðer manuel atama yapmýyorsanýz, burayý kullanacaðýz.

    void Start()
    {
        timeRemaining = totalTime;

        //Find the Directional Lights and turn them off
        Light[] lights = FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                light.enabled = false;
            }
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; 
                UpdateTimerDisplay();

                //Start the warning when there are 5 minutes left 
                if (timeRemaining <= 300f && !isWarningActive) // 300 sec 
                {
                    isWarningActive = true;
                    StartCoroutine(WarningEffect());
                }
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                TimerEnded(); 
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Formatted time string
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Make the text red in the last 5 minutes
        if (timeRemaining <= 300f)
        {
            timerText.color = Color.red;
        }
    }

    private IEnumerator WarningEffect()
    {
        //The warning will only be active during the last 5 minutes. It hides the text, waits for 0.5 seconds, shows the text, then waits for 0.5 seconds again.
        while (timeRemaining > 0 && timeRemaining <= 300f) 
        {
            timerText.enabled = false; 
            yield return new WaitForSeconds(0.5f); 
            timerText.enabled = true; 
            yield return new WaitForSeconds(0.5f); 
        }
    }
    void TimerEnded()
    {
        Debug.Log("Time's up!"); // for test
        Time.timeScale = 0f; //Stop the game timer
        gameOverPanel.SetActive(true); // Show the game over panel

    }

    /*   void TimerEnded()
       {
           Debug.Log("Time's up!"); // for test
           Time.timeScale = 0f; //Stop the game timer
           gameOverPanel.SetActive(true); // Show the game over panel

           // Game over mesajýný PasswordManager'a gönder
           if (passwordManager != null)
           {
               passwordManager.UpdateKeyToGameOver(); // key'yi "Game Over" olarak ayarla
           }
       }*/




    public void Win()
    {
        // Activate all the lights in the scene again
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                light.enabled = true;
            }
        }
        isTimerRunning = false; // Stop the timer
        StopAllCoroutines(); // Stop the warning effect
        Debug.Log("The player won!"); // for test
        StartCoroutine(ShowWinnerPanel());
    }


    private IEnumerator ShowWinnerPanel()
    {
        //Show the win panel on the screen 15 seconds after the game is won (this 15 seconds allows the player to exit and see it)
        yield return new WaitForSeconds(15f); 
        winnerPanel.SetActive(true);
      
        if (cameraController != null)
        {
            cameraController.enabled = false; //the player's movement is stopped
        }
        Time.timeScale = 0f; //Stop the game timer
    }
}
