using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    private PlayerEvolution playerEvolution;   // Reference to the player's evolution component
    private GameManager gameManager;           // Reference to the GameManager
    private Renderer vehicleRenderer;          // Reference to the vehicle's renderer

    private bool cooldownActive = false;       // Flag to avoid multiple collisions in a short time
    public float cooldownTime = 0.2f;          // Cooldown time to prevent multiple collision detections
    public int blinkCount = 5;                 // Number of blinks to indicate a collision
    public float blinkDuration = 0.1f;         // Duration of each blink during collision feedback

    public AudioClip obstacleSound;            // Sound clip for obstacle collision
    private AudioSource audioSource;           // Audio source to play sounds

    private void Start()
    {
        // Get the PlayerEvolution component from the parent object
        playerEvolution = GetComponentInParent<PlayerEvolution>();
        if (playerEvolution == null)
        {
            Debug.LogError("PlayerEvolution not found in the object or parent object.");
        }

        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        // Get the Renderer component for the vehicle to use for blinking effect
        vehicleRenderer = GetComponentInParent<Renderer>();
        if (vehicleRenderer == null)
        {
            Debug.LogError("Renderer not found in the parent object.");
        }

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Exit if cooldown is active to prevent multiple detections
        if (cooldownActive) return;

        // Check if the collided object is tagged as "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            PlaySound(obstacleSound);  // Play obstacle collision sound
            StartCoroutine(CooldownRoutine());  // Start cooldown to prevent multiple collisions

            if (playerEvolution != null && playerEvolution.CurrentEvolution > 0)
            {
                playerEvolution.RevertEvolution();  // Revert player's evolution level
                StartCoroutine(BlinkEffect());      // Trigger blinking effect for feedback
            }
            else
            {
                Debug.Log("Minimum level reached. Game over.");
                StopGame();  // Stop the game if player is at minimum evolution level
            }
        }
    }

    private void StopGame()
    {
        // Call the GameManager's StopGame method to end the game
        if (gameManager != null)
        {
            gameManager.StopGame();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        // Play the provided audio clip if AudioSource and clip are not null
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator CooldownRoutine()
    {
        cooldownActive = true;  // Enable cooldown
        yield return new WaitForSeconds(cooldownTime);  // Wait for cooldown duration
        cooldownActive = false; // Disable cooldown
    }

    private IEnumerator BlinkEffect()
    {
        // Blink the vehicleRenderer on and off for the specified number of times
        for (int i = 0; i < blinkCount; i++)
        {
            vehicleRenderer.enabled = false;  // Hide the vehicle
            yield return new WaitForSeconds(blinkDuration);
            vehicleRenderer.enabled = true;   // Show the vehicle
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
