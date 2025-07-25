using UnityEngine;

public class TouchPanZoom2D : MonoBehaviour
{
    public float panSpeed = 0.01f;
    public float zoomSpeed = 0.5f;
    public float minSize = 2f;
    public float maxSize = 20f;

    private Camera cam;
    private Vector3 lastMousePos;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (GlobalTouchState.IsInteractingWithObject)
            return;

#if UNITY_EDITOR || UNITY_STANDALONE
        // Mouse drag to pan
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            transform.Translate(-delta.x * panSpeed, -delta.y * panSpeed, 0);
            lastMousePos = Input.mousePosition;
        }

        // Mouse scroll to zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scroll * zoomSpeed * 100f * Time.deltaTime;
#else
        // Touch pan
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                Vector3 move = new Vector3(-t.deltaPosition.x * panSpeed, -t.deltaPosition.y * panSpeed, 0);
                transform.Translate(move, Space.Self);
            }
        }
        // Touch zoom
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevPos0 = t0.position - t0.deltaPosition;
            Vector2 prevPos1 = t1.position - t1.deltaPosition;

            float prevDistance = (prevPos0 - prevPos1).magnitude;
            float currentDistance = (t0.position - t1.position).magnitude;

            float delta = currentDistance - prevDistance;
            cam.orthographicSize -= delta * zoomSpeed * Time.deltaTime;
        }
#endif

        // Clamp zoom size
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
    }
}
