using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalTouchState
{
    public static bool IsInteractingWithObject = false;

    public static void SetInteraction(bool state)
    {
        IsInteractingWithObject = state;
    }
}
