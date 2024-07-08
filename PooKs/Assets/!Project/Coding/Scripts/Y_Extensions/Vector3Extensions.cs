using UnityEngine;


public static class Vector3Extensions
{
    /// <summary>
    ///     <para>Switches the value of an axis in a given Vector3</para>
    /// </summary>
    public static Vector3 Switch(this Vector3 vector, Axis axis, float value)
    {
        Vector3 switchedVector;
        switchedVector.x = axis == Axis.X ? value : vector.x;
        switchedVector.y = axis == Axis.Y ? value : vector.y;
        switchedVector.z = axis == Axis.Z ? value : vector.z;
        return switchedVector;
    }
    
    /// <summary>
    ///     <para>Turns all floats to int of a Vector3</para>
    /// </summary>
    public static Vector3 Int(this Vector3 vector)
    {
        Vector3 intVector3;
        intVector3.x = (int)vector.x;
        intVector3.y = (int)vector.y;
        intVector3.z = (int)vector.z;
        return intVector3;
    }
    
    /// <summary>
    ///     <para>Returns the next contact position on a Raycast hit, returns a Vector3.Zero if the Raycast doesnt hit anything</para>
    /// </summary>
    public static Vector3 LayerHit(this Vector3 vector , LayerMask layer , Vector3 direction)
    {
        Ray centerRay = new Ray(vector, direction);
        if (Physics.Raycast(centerRay, out RaycastHit hitInfo, 200f, layer))
        {
            Vector3 centerPos;
            centerPos.x = hitInfo.point.x;
            centerPos.y = hitInfo.point.y;
            centerPos.z = hitInfo.point.z;
            
            return centerPos;
        }
        return Vector3.zero;
    }
}

public enum Axis {X,Y,Z}
