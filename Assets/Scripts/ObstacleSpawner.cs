using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Prefab for the obstacle
    public float spawnInterval = 2f;   // Time interval between obstacle spawns
    public float initialDelay = 3f;    // Initial delay before the first obstacle spawns
    public float minX = -3f;           // Minimum X limit for obstacle spawn position
    public float maxX = 3f;            // Maximum X limit for obstacle spawn position
    public float spawnY = 6f;          // Y position where obstacles will spawn

    private void Start()
    {
        // Start the obstacle spawner with an initial delay and repeated spawn interval
        InvokeRepeating("SpawnObstacle", initialDelay, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Generate a random X position within the specified limits
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Instantiate an obstacle at the generated spawn position
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
