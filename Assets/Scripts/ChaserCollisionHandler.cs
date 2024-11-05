using UnityEngine;

public class ChaserCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with this one has the "Chaser" tag
        if (other.CompareTag("Chaser"))
        {
            Debug.Log("The Chaser has reached the player. Game over.");
            // Find the GameManager in the scene and call StopGame to end the game
            FindObjectOfType<GameManager>().StopGame();
        }
    }
}
