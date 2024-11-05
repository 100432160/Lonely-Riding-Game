using UnityEngine;

public class EvolverSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;      // Power-up (Evolver) prefab to spawn
    public float spawnInterval = 5f;      // Time interval between power-up spawns
    public float initialDelay = 5f;       // Initial wait time before the first power-up spawn
    public float minX = -3f;              // Minimum X position for spawning power-ups
    public float maxX = 3f;               // Maximum X position for spawning power-ups
    public float spawnY = 6f;             // Y position where power-ups will spawn
    public PlayerEvolution playerEvolution; // Reference to the player's PlayerEvolution script

    private void Start()
    {
        // Repeatedly call SpawnPowerUp method after the initial delay and at each spawn interval
        InvokeRepeating("SpawnPowerUp", initialDelay, spawnInterval);
    }

    void SpawnPowerUp()
    {
        // Log the current evolution level and maximum evolution level
        Debug.Log("Current Evolution Level: " + playerEvolution.CurrentEvolution);
        Debug.Log("Max Evolution Level: " + (playerEvolution.evolutionColors.Length - 1));

        // Check if the player has not reached the maximum evolution level
        if (playerEvolution != null && playerEvolution.CurrentEvolution < playerEvolution.evolutionColors.Length - 1)
        {
            // Randomly select an X position within the specified range
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

            // Instantiate the power-up at the generated position
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Log message indicating the max evolution level has been reached, and stop spawning
            Debug.Log("Maximum evolution level reached. No more Evolvers will be spawned.");
            CancelInvoke("SpawnPowerUp"); // Stop the repeated SpawnPowerUp calls
        }
    }
}
