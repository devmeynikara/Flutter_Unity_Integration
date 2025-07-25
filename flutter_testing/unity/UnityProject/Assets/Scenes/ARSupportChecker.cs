using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class ARSupportChecker : MonoBehaviour
{   
    [Header("Events")]
    public UnityEvent arEvents;
    public UnityEvent fallback3DEvents;
    IEnumerator Start()
    {
        if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported || ARSession.state == ARSessionState.NeedsInstall)
        {
            Debug.Log("AR not supported — switching to 3D view.");
            TriggerFallback3D();
        }
        else
        {
            Debug.Log("AR supported — enabling AR experience.");
            TriggerAREnabled();
        }
    }

    public void TriggerAREnabled()
    {
        arEvents?.Invoke();
    }

    public void TriggerFallback3D()
    {
        fallback3DEvents?.Invoke();
    }

    public GameObject objectToDestroy;

    public void OnButtonClick()
    {
        DestroyTarget(objectToDestroy);
    }

    public void DestroyTarget(GameObject target)
    {
        if (target != null)
        {
            Destroy(target);
        }
        else
        {
            Debug.LogWarning("DestroyTarget: Target GameObject is null.");
        }
    }

}
