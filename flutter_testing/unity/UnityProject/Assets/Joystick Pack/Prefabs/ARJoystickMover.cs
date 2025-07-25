using UnityEngine;

public class ARCameraJoystickControl : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform arCamera;
    public float moveSpeed = 1.5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        if (arCamera != null)
        {
            initialPosition = arCamera.position;
            initialRotation = arCamera.rotation;
        }
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDir.magnitude > 0.1f)
        {
            // Move relative to current Y rotation only
            Vector3 moveDir = Quaternion.Euler(0f, arCamera.eulerAngles.y, 0f) * inputDir;
            arCamera.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    // Reset camera to original state
    public void ResetARCamera()
    {
        if (arCamera != null)
        {
            arCamera.position = initialPosition;
            arCamera.rotation = initialRotation;
        }
    }
}
