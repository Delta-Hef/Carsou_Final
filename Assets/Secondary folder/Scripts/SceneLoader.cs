using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public Transform player;
    private bool isGameOver = false;
    public TextMeshProUGUI gameOverrrText;
    public Button restart;
    // Start is called before the first frame update

    void Start()
    {
        // Ensure the Game Over text is initially inactive
        if (gameOverrrText != null)
        {
            gameOverrrText.gameObject.SetActive(false);
            restart.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(0);
        }

        if (!isGameOver && player.position.y < -2)
        {
            TriggerGameOver();
        }

        if (!isGameOver && player.position.y > 10)
        {
            TriggerGameOver();
        }
    }
    public void LoadScen55e()
    {
        SceneManager.LoadScene(0);
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        // Display the Game Over text
        if (gameOverrrText != null)
        {
            gameOverrrText.gameObject.SetActive(true);
            //restart.gameObject.SetActive(true);
        }

        // Optional: Pause the game
        Time.timeScale = 0f;
    }
}
