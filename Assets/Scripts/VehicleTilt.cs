using UnityEngine;

public class VehicleTilt : MonoBehaviour
{
    public float tiltAngle = 15f;         // Maximum tilt angle
    public float tiltSpeed = 5f;          // Transition speed for smooth tilting
    public Transform vehicle;             // Public reference to the child object (vehicle)

    void Start()
    {
        // If the reference is not manually assigned, try to get the first child as the vehicle
        if (vehicle == null && transform.childCount > 0)
        {
            vehicle = transform.GetChild(0);
        }

        // Display an error if no vehicle child object is assigned
        if (vehicle == null)
        {
            Debug.LogError("Child object (vehicle) is not assigned. Make sure to assign it in the Inspector.");
        }
    }

    void Update()
    {
        // Exit if no vehicle is assigned
        if (vehicle == null) return;

        // Detect horizontal movement input (left or right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Determine the target tilt angle based on input direction
        float targetTilt = 0f;
        if (horizontalInput > 0)
        {
            targetTilt = -tiltAngle;  // Tilt right
        }
        else if (horizontalInput < 0)
        {
            targetTilt = tiltAngle;   // Tilt left
        }

        // Smoothly interpolate the rotation using Lerp and apply only to the child object
        float currentTilt = Mathf.LerpAngle(vehicle.localEulerAngles.z, targetTilt, tiltSpeed * Time.deltaTime);
        vehicle.localRotation = Quaternion.Euler(0, 0, currentTilt);
    }
}
