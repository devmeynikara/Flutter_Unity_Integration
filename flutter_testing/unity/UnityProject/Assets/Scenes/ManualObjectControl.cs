using UnityEngine;

public class ManualObjectControl : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float yLock;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
        yLock = transform.position.y;
    }

    void Update()
    {
        if (Input.touchSupported && Input.touchCount > 0)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedThisObject(Input.mousePosition))
            {
                isDragging = true;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    offset = transform.position - hit.point;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 target = hit.point + offset;
                target.y = yLock;
                transform.position = target;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (IsClickedThisObject(touch.position))
                {
                    isDragging = true;
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        offset = transform.position - hit.point;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 target = hit.point + offset;
                    target.y = yLock;
                    transform.position = target;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    bool IsClickedThisObject(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }
        return false;
    }
}
