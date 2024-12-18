using UnityEngine;
using TMPro; // For TextMeshPro support

public class LogicoScript : MonoBehaviour
{
    public float countdownTime = 30f; // Countdown start time in seconds
    public TextMeshProUGUI timerText;      // Drag your Timer TextMeshPro object here
    public TextMeshProUGUI gameOverText;   // Drag your GameOver TextMeshPro object here

    void Start()
    {
        // Hide the Game Over text initially
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Countdown logic
        countdownTime -= Time.deltaTime;

        // Clamp the timer at zero
        if (countdownTime <= 0)
        {
            countdownTime = 0;

            // Show "Game Over" message
            if (gameOverText != null)
            {
                gameOverText.text = "Game Over";
                gameOverText.gameObject.SetActive(true);
            }
        }

        // Update the timer display with "Timer: X" format
        if (timerText != null)
            timerText.text = "Timer: " + Mathf.Ceil(countdownTime).ToString();
    }
}
