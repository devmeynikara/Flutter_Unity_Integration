using UnityEngine;
using Lean.Touch;
using Lean.Common;

public class LeanObjectSelectionHandler : MonoBehaviour
{
    private LeanSelectableByFinger selectable;
    private LeanDragTranslate drag;
    private LeanTwistRotateAxis rotator;
    private bool isTouching = false;

    private void Awake()
    {
        selectable = GetComponent<LeanSelectableByFinger>();
        drag = GetComponent<LeanDragTranslate>();
        rotator = GetComponent<LeanTwistRotateAxis>();

        // Disable movement/rotation initially
        if (drag != null) drag.enabled = false;
        if (rotator != null) rotator.enabled = false;
    }

    void Update()
    {
        // TOUCH INPUT
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform == transform)
                    {
                        isTouching = true;
                        GlobalTouchState.SetInteraction(true);

                        if (selectable != null) selectable.SelfSelected = true;
                        if (drag != null) drag.enabled = true;
                        if (rotator != null) rotator.enabled = true;
                    }
                }
            }

            if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isTouching)
            {
                isTouching = false;
                GlobalTouchState.SetInteraction(false);

                if (selectable != null) selectable.SelfSelected = false;
                if (drag != null) drag.enabled = false;
                if (rotator != null) rotator.enabled = false;
            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        // MOUSE INPUT
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isTouching = true;
                    GlobalTouchState.SetInteraction(true);

                    if (selectable != null) selectable.SelfSelected = true;
                    if (drag != null) drag.enabled = true;
                    if (rotator != null) rotator.enabled = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            isTouching = false;
            GlobalTouchState.SetInteraction(false);

            if (selectable != null) selectable.SelfSelected = false;
            if (drag != null) drag.enabled = false;
            if (rotator != null) rotator.enabled = false;
        }
#endif
    }
}
