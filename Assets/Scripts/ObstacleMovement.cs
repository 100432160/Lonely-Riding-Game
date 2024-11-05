using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    void Update()
    {
        // Move the obstacle downwards using the global game speed
        transform.Translate(Vector3.down * GameManager.globalSpeed * Time.deltaTime);

        // Destroy the obstacle when it moves off the screen
        if (transform.position.y < -6f) // Adjust this limit based on the screen size
        {
            Destroy(gameObject);
        }
    }
}
