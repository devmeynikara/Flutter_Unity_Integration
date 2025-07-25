using UnityEngine;

public class TouchCameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float zoomSpeed = 0.5f;
    public float minDistance = 2f;
    public float maxDistance = 20f;
    public float rotationSpeed = 0.2f;
    public float panSpeed = 0.005f;

    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3 lastPanPosition;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (GlobalTouchState.IsInteractingWithObject)
            return;

#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif

        pitch = Mathf.Clamp(pitch, -80f, 80f);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 negDistance = new Vector3(0f, 0f, -distance);
        transform.position = target.position + rotation * negDistance;
        transform.rotation = rotation;
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                yaw += t.deltaPosition.x * rotationSpeed * 0.1f;
                pitch -= t.deltaPosition.y * rotationSpeed * 0.1f;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevT0 = t0.position - t0.deltaPosition;
            Vector2 prevT1 = t1.position - t1.deltaPosition;
            float prevMag = (prevT0 - prevT1).magnitude;
            float currMag = (t0.position - t1.position).magnitude;

            float diff = currMag - prevMag;
            distance -= diff * zoomSpeed * 0.01f;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Vector2 avgDelta = (t0.deltaPosition + t1.deltaPosition) / 2f;
            PanCamera(avgDelta);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * 100f * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * 100f * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastPanPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastPanPosition;
            PanCamera(delta);
            lastPanPosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed * 100f * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void PanCamera(Vector2 delta)
    {
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Vector3 move = (-right * delta.x - up * delta.y) * panSpeed;
        target.position += move;
    }
}
