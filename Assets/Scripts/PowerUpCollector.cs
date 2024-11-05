using UnityEngine;
using System.Collections;

public class PowerUpCollector : MonoBehaviour
{
    private PlayerMovement playerMovement;  // Reference to the player's movement script
    private PlayerEvolution playerEvolution; // Reference to the player's evolution script

    private bool cooldownActive = false;     // To prevent multiple triggers
    public float cooldownTime = 0.2f;        // Cooldown time between consecutive collisions

    public AudioClip powerUpSound;           // Sound for collecting a power-up
    public AudioClip evolverSound;           // Sound for collecting an evolver
    private AudioSource audioSource;         // Audio source for playing sounds

    private void Start()
    {
        // Get references to PlayerMovement and PlayerEvolution scripts on the parent object
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerEvolution = GetComponentInParent<PlayerEvolution>();

        if (playerMovement == null || playerEvolution == null)
        {
            Debug.LogError("PlayerMovement or PlayerEvolution not found on parent object.");
        }

        // Get or add an AudioSource component for sound effects
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Exit if cooldown is active
        if (cooldownActive) return;

        if (other.CompareTag("PowerUp"))
        {
            StartCoroutine(CooldownRoutine()); // Start cooldown to prevent multiple triggers
            PlaySound(powerUpSound);           // Play the power-up sound

            if (playerMovement != null)
            {
                playerMovement.AddSprint();    // Add a sprint to the player's available sprints
            }
            Destroy(other.gameObject);         // Destroy the power-up object
        }
        else if (other.CompareTag("Evolver"))
        {
            StartCoroutine(CooldownRoutine()); // Start cooldown to prevent multiple triggers
            PlaySound(evolverSound);           // Play the evolver sound
            
            if (playerEvolution != null)
            {
                playerEvolution.Evolve();      // Evolve the player to the next level
            }
            Destroy(other.gameObject);         // Destroy the evolver object
        }
    }

    // Method to play the specified sound clip
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Cooldown coroutine to prevent immediate repeated triggers
    private IEnumerator CooldownRoutine()
    {
        cooldownActive = true;                  // Activate cooldown
        yield return new WaitForSeconds(cooldownTime); // Wait for the cooldown time
        cooldownActive = false;                 // Deactivate cooldown
    }
}
