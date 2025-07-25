using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LeanTouchBoundsRestrictor : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private bool boundsInitialized = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Try to find the floor at runtime by tag
        GameObject floor = GameObject.FindWithTag("Floor");
        if (floor != null)
        {
            Collider floorCollider = floor.GetComponent<Collider>();
            if (floorCollider != null)
            {
                Bounds bounds = floorCollider.bounds;
                minBounds = new Vector2(bounds.min.x, bounds.min.z);
                maxBounds = new Vector2(bounds.max.x, bounds.max.z);
                boundsInitialized = true;
            }
            else
            {
                Debug.LogError("Floor object found but has no Collider!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Floor' found in the scene.");
        }
    }

    void FixedUpdate()
    {
        if (!boundsInitialized) return;

        Vector3 pos = rb.position;

        // Clamp within floor bounds (XZ plane)
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.z = Mathf.Clamp(pos.z, minBounds.y, maxBounds.y);

        // Optional: Lock or clamp Y to avoid floating
        pos.y = Mathf.Clamp(pos.y, 0f, 0.1f); // adjust based on your floor height

        rb.MovePosition(pos);
    }
}
