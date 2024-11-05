using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;   // Prefab of the power-up
    public float spawnInterval = 5f;   // Time interval between power-up spawns
    public float initialDelay = 5f;    // Initial delay before the first power-up spawn
    public float minX = -3f;           // Minimum X position for spawning power-ups
    public float maxX = 3f;            // Maximum X position for spawning power-ups
    public float spawnY = 6f;          // Y position where power-ups appear

    private void Start()
    {
        // Start the power-up spawner with an initial delay
        InvokeRepeating("SpawnPowerUp", initialDelay, spawnInterval);
    }

    void SpawnPowerUp()
    {
        // Generate a random X position within the specified limits
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Instantiate the power-up at the generated position
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}
