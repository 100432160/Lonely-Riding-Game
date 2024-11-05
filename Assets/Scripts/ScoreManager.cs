using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float scoreMultiplier = 1f;              // Multiplier for the score based on the game's speed
    public TextMeshProUGUI scoreText;               // Reference to the UI text for displaying the current score
    public TextMeshProUGUI highScoreText;           // Reference to the UI text for displaying the high score

    private float score = 0f;                       // Current score
    private int highScore;                          // High score
    private bool isGameRunning = true;              // Indicator of whether the game is active

    public float CurrentScore => score;             // Public property to get the current score
    public int HighScore => highScore;              // Public property to get the high score

    void Start()
    {
        // Load the saved high score and update the high score UI
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }

    void Update()
    {
        // Update the score only if the game is active
        if (isGameRunning)
        {
            // Increase the score based on time, global speed, and the score multiplier
            score += Time.deltaTime * GameManager.globalSpeed * scoreMultiplier;

            // Check if the new score exceeds the high score
            if (Mathf.FloorToInt(score) > highScore)
            {
                highScore = Mathf.FloorToInt(score);
                PlayerPrefs.SetInt("HighScore", highScore);  // Save the new high score
                UpdateHighScoreUI();  // Update the high score UI text
            }

            // Update the current score UI text
            UpdateScoreUI();
        }
    }

    // Method to update the UI text with the current score
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    // Method to update the UI text with the high score
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    // Method to stop updating the score when the game ends
    public void StopScoring()
    {
        isGameRunning = false;
    }

    // Method to reset the score when restarting the game
    public void ResetScore()
    {
        score = 0f;
        isGameRunning = true;
        UpdateScoreUI();
    }

    private void OnApplicationQuit()
    {
        // Save the high score when the application closes
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}
