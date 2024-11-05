using UnityEngine;

public class PlayerEvolution : MonoBehaviour
{
    public Color[] evolutionColors;         // Array of colors for each evolution stage
    public float speedIncrement = 2f;       // Speed increment with each evolution
    private int currentEvolution = 0;       // Index of the current evolution stage
    private Renderer vehicleRenderer;       // Reference to the player's Renderer

    public int CurrentEvolution => currentEvolution; // Public property to access currentEvolution

    private int lives;                      // Life counter
    private LifeUIManager lifeUIManager;    // Reference to the script managing the life UI

    private void Start()
    {
        vehicleRenderer = GetComponent<Renderer>();

        // Set the initial color based on level 0, if colors are available
        if (evolutionColors.Length > 0)
        {
            currentEvolution = 0;
            vehicleRenderer.material.color = evolutionColors[currentEvolution];
        }
        else
        {
            Debug.LogWarning("The evolutionColors array is empty. Ensure colors are assigned in the Inspector.");
        }

        // Initialize LifeUIManager and update lives at the start
        lifeUIManager = FindObjectOfType<LifeUIManager>();
        UpdateLives();
    }

    public void Evolve()
    {
        // Evolve to the next level if not already at the max evolution level
        if (currentEvolution < evolutionColors.Length - 1)
        {
            currentEvolution++;
            vehicleRenderer.material.color = evolutionColors[currentEvolution];
            GameManager.globalSpeed += speedIncrement;
            Debug.Log($"EVOLVE. New level: {currentEvolution}, New speed: {GameManager.globalSpeed}");

            UpdateLives(); // Update the life counter and UI
        }
        else
        {
            Debug.Log("You have reached the maximum evolution level.");
        }
    }

    public void RevertEvolution()
    {
        // Revert to the previous evolution level if not already at the minimum level
        if (currentEvolution > 0)
        {
            currentEvolution--;
            vehicleRenderer.material.color = evolutionColors[currentEvolution];
            GameManager.globalSpeed -= speedIncrement;
            Debug.Log($"OBSTACLE. New level: {currentEvolution}, New speed: {GameManager.globalSpeed}");

            UpdateLives(); // Update the life counter and UI
        }
        else
        {
            Debug.Log("Minimum level reached. Cannot reduce further.");
            FindObjectOfType<GameManager>().StopGame();
        }
    }

    private void UpdateLives()
    {
        // Set the number of lives based on the evolution level
        lives = currentEvolution + 1; 
        lifeUIManager.UpdateLivesUI(lives); // Update the life container UI with sprites
    }
}
