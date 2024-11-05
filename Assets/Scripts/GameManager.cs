using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static float globalSpeed = 5f;  // Global speed used by both the road and obstacles
    private bool isGameStopped = false;    // Flag to indicate if the game is stopped

    public GameObject gameOverPanel;       // Reference to the Game Over panel
    public TextMeshProUGUI highScoreText;  // Text to display the High Score on the Game Over panel
    public TextMeshProUGUI scoreText;      // Text to display the current Score on the Game Over panel

    private ScoreManager scoreManager;     // Reference to the ScoreManager

    void Start()
    {
        // Hide the Game Over panel at the start of the game
        gameOverPanel.SetActive(false);

        // Get the reference to the ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    void Update()
    {
        // Detect the 'R' key to restart the game only if the game is stopped
        if (isGameStopped && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Method to update the global speed (optional, in case you need to change it dynamically)
    public void SetGlobalSpeed(float newSpeed)
    {
        globalSpeed = newSpeed;
    }

    // Method to stop the game
    public void StopGame()
    {
        globalSpeed = 0f;           // Stop the global speed
        Time.timeScale = 0f;        // Pause the entire game
        isGameStopped = true;       // Mark the game as stopped
        Debug.Log("Game stopped. Press 'R' to restart.");

        // Update the Game Over panel with the current Score and High Score
        if (scoreManager != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(scoreManager.CurrentScore).ToString();
            highScoreText.text = "High Score: " + scoreManager.HighScore.ToString();
        }

        // Show the Game Over panel
        gameOverPanel.SetActive(true);
    }

    // Method to restart the game
    public void RestartGame()
    {
        globalSpeed = 5f;           // Reset global speed to its initial value
        Time.timeScale = 1f;        // Resume time
        isGameStopped = false;      // Mark the game as not stopped
        gameOverPanel.SetActive(false); // Hide the Game Over panel

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
