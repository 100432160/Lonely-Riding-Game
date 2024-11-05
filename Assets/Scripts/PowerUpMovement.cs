using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    void Update()
    {
        // Move the power-up downward using the global game speed
        transform.Translate(Vector3.down * GameManager.globalSpeed * Time.deltaTime);

        // Destroy the power-up when it goes off the screen
        if (transform.position.y < -6f)  // Adjust the limit according to the scene
        {
            Destroy(gameObject);
        }
    }
}
