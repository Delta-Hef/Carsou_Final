using UnityEngine;
using TMPro; // For TextMeshPro UI

public class MoneyScore : MonoBehaviour
{
    [Header("UI Settings")]
    public TMP_Text scoreText; // Reference to the TextMeshPro UI Text

    private int score = 0; // The player's score

    void Start()
    {
        // Automatically find the TextMeshPro UI component if not manually assigned
        if (scoreText == null)
        {
            scoreText = GameObject.Find("MoneyScoreText").GetComponent<TMP_Text>();
        }

        // Initialize the score display
        UpdateScoreText();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with an object tagged "Money"
        if (other.CompareTag("Money"))
        {
            // Increase the score
            score += 1;

            // Update the score display
            UpdateScoreText();

            // Optionally destroy the money object after collecting it
            Destroy(other.gameObject);
        }
    }

    void UpdateScoreText()
    {
        // Update the UI Text to display the current score
        scoreText.text = "Money: " + score.ToString();
    }
}
