using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject otherRoad;      // Reference to the other road GameObject
    public float gapOffset = 0.1f;    // Adjustment to eliminate any gap between roads

    private float roadHeight;         // Effective height of the road sprite

    void Start()
    {
        // Find the SpriteRenderer component in the children of this GameObject
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Calculate the effective height of the sprite, taking scaling into account
            roadHeight = spriteRenderer.bounds.size.y;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found in children of " + gameObject.name);
        }
    }

    void Update()
    {
        // Move the road downwards using the global game speed
        transform.Translate(Vector3.down * GameManager.globalSpeed * Time.deltaTime);

        // Check if the road has moved below the screen; reposition it if needed
        if (transform.position.y < -roadHeight)
        {
            RepositionAboveOtherRoad();
        }
    }

    void RepositionAboveOtherRoad()
    {
        // Reposition this road GameObject just above the other road,
        // with an adjustment to eliminate any gap
        transform.position = new Vector3(transform.position.x,
                                         otherRoad.transform.position.y + roadHeight - gapOffset,
                                         transform.position.z);
    }
}
