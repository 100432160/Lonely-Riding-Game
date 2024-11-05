using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;      // Reference to the Game Over panel
    public TextMeshProUGUI highScoreText; // Reference to the High Score text
    public TextMeshProUGUI scoreText;     // Reference to the Score text
    private bool isGameOver = false;      // Controls if the game is over

    private void Start()
    {
        gameOverPanel.SetActive(false);   // Hide the Game Over panel at the start of the game
    }

    private void Update()
    {
        // Check if 'R' key is pressed and the game has ended
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void ShowGameOverPanel(float score, float highScore)
    {
        isGameOver = true;  // Mark the game as ended
        gameOverPanel.SetActive(true); // Show the Game Over panel

        // Update the UI texts with the final score and high score
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    private void RestartGame()
    {
        isGameOver = false;           // Reset the game-over status
        gameOverPanel.SetActive(false); // Hide the Game Over panel
        FindObjectOfType<GameManager>().RestartGame(); // Call the restart method in GameManager
    }
}
