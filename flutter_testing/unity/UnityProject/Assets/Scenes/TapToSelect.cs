using UnityEngine;
using Lean.Touch;

public class TapToSelect : MonoBehaviour
{
    public LayerMask selectableLayer;

    private void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleTap;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerTap -= HandleTap;
    }

    private void HandleTap(LeanFinger finger)
    {
        var ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectableLayer))
        {
            var newSelectable = hit.collider.GetComponent<LeanSelectableByFinger>();

            // Deselect all
            LeanSelectableByFinger.DeselectAll();

            if (newSelectable != null)
            {
               // newSelectable.Select();
            }
        }
        else
        {
            // Deselect if clicked on empty space
            LeanSelectableByFinger.DeselectAll();
        }
    }
}
