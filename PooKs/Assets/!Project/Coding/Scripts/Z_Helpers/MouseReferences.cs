using UnityEngine;
using UnityEngine.InputSystem;

public static class MouseReferences
{
    /// <summary>
    ///   <para>Returns true if the mouse has moved more than the given distance</para>
    /// </summary>
    /// <param name="Mouse Input"></param>
    /// <param name="Last Mouse Position"></param>
    /// <param name="Minimun Distance"></param>
    /// <returns></returns>
    public static bool CheckMouseDisplacement(InputAction.CallbackContext context, Vector3 lastMousePos , float minDistance)
    {
        Vector2 value = context.ReadValue<Vector2>();
        float mouseMovedDistance = Vector3.Distance(value, lastMousePos);
        if (mouseMovedDistance > minDistance) return true;
        return false;
    }
}