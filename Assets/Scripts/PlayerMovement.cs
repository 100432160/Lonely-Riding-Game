using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;                    // Player's movement speed
    public float xMin = -8f;                     // Left boundary for movement
    public float xMax = 8f;                      // Right boundary for movement
    public float sprintDistance = 2f;            // Distance the player advances with a sprint
    public float sprintCooldown = 1f;            // Cooldown time between sprints
    public float sprintSpeed = 5f;               // Sprint speed
    public float backwardSpeed = 0.5f;           // Backward movement speed on the Y-axis
    public float initialDelay = 3f;              // Grace period before the player starts moving backward

    private float lastSprintTime;                // Tracks the last sprint time
    private bool isSprinting = false;            // Tracks if the player is currently sprinting
    private bool isInitialDelayOver = false;     // Tracks if the grace period has ended
    private float sprintTargetY;                 // Y position target for sprint

    public int availableSprints = 0;             // Counter for available sprints
    public TextMeshProUGUI sprintCounterText;    // UI text for displaying sprint count

    public AudioClip sprintSound;                // Sprint sound effect
    private AudioSource audioSource;             // Audio source for playing sound effects

    private void Start()
    {
        // Start the grace period countdown
        Invoke("EndInitialDelay", initialDelay);

        // Get or add an AudioSource component for sound effects
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Ends the initial grace period
    private void EndInitialDelay()
    {
        isInitialDelayOver = true;
    }

    void Update()
    {
        // Horizontal movement based on user input
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;

        transform.Translate(movement);

        // Clamp movement to the left and right boundaries
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);

        // Gradual backward movement on the Y-axis (only after the grace period and if not sprinting)
        float newYPosition = transform.position.y;
        if (isInitialDelayOver && !isSprinting)
        {
            newYPosition -= backwardSpeed * Time.deltaTime;
        }

        // Sprint activation: check if a sprint key is pressed, cooldown is met, and sprints are available
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) &&
            Time.time >= lastSprintTime + sprintCooldown && !isSprinting && availableSprints > 0)
        {
            lastSprintTime = Time.time;
            isSprinting = true;
            sprintTargetY = transform.position.y + sprintDistance; // Set sprint target position on Y-axis
            availableSprints--;
            UpdateSprintCounterUI();
            PlaySound(sprintSound); // Play sprint sound effect
            Debug.Log("Sprint activated!");
        }

        // Smooth movement towards the sprint target if sprinting
        if (isSprinting)
        {
            float newY = Mathf.Lerp(transform.position.y, sprintTargetY, sprintSpeed * Time.deltaTime);
            transform.position = new Vector3(clampedX, newY, transform.position.z);

            // Check if the player has reached the sprint target position on the Y-axis
            if (Mathf.Abs(transform.position.y - sprintTargetY) < 0.1f)
            {
                isSprinting = false;
            }
        }
        else
        {
            // Apply new Y position with backward movement or hold during the grace period
            transform.position = new Vector3(clampedX, newYPosition, transform.position.z);
        }
    }

    // Method to play sound effects
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Method to update the sprint count UI
    private void UpdateSprintCounterUI()
    {
        if (sprintCounterText != null)
        {
            sprintCounterText.text = "Sprints: " + availableSprints;
        }
    }

    // Method to increment the sprint counter when a sprint power-up is collected
    public void AddSprint()
    {
        availableSprints++;
        UpdateSprintCounterUI();
    }
}
