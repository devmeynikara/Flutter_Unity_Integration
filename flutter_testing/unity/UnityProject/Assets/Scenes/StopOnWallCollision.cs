using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StopOnWallCollision : MonoBehaviour
{
    private bool isBlocked = false;
    private Vector3 lastValidPosition;

    void Start()
    {
        lastValidPosition = transform.position;
    }

    void Update()
    {
        if (!isBlocked)
        {
            lastValidPosition = transform.position;
        }
        else
        {
            transform.position = lastValidPosition; // snap back to last valid position
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            isBlocked = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            isBlocked = false;
        }
    }
}
