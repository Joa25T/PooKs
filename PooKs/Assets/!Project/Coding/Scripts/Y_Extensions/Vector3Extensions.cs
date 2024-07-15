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
    /// <para> Zero's the given axis</para>
    /// </summary>
    public static Vector3 ZeroAxis(this Vector3 vector , Axis axis)
    {
        return Switch(vector, axis, 0);
    }
    /// <summary>
    ///     <para>Adds value to an axis in a given Vector3</para>
    /// </summary>
    public static Vector3 AddValue(this Vector3 vector, Axis axis, float value)
    {
        Vector3 switchedVector;
        switchedVector.x = axis == Axis.X ? vector.x + value : vector.x;
        switchedVector.y = axis == Axis.Y ? vector.y + value : vector.y;
        switchedVector.z = axis == Axis.Z ? vector.z + value : vector.z;
        return switchedVector;
    }
    
    /// <summary>
    ///     <para>Turns all floats to int of a Vector3</para>
    /// </summary>
    /// 
    public static Vector3 Int(this Vector3 vector)
    {
        Vector3 intVector3;
        intVector3.x = (int)vector.x;
        intVector3.y = (int)vector.y;
        intVector3.z = (int)vector.z;
        return intVector3;
    }
    public static Vector3 Int(this Vector3 vector, Axis axis)
    {
        Vector3 intVector3;
        intVector3.x = axis == Axis.X ? (int)vector.x : vector.x;
        intVector3.y = axis == Axis.Y ? (int)vector.y : vector.y;
        intVector3.z = axis == Axis.Z ? (int)vector.z : vector.z;
        return intVector3;
    }

    
    /// <summary>
    ///     <para>Returns the next contact position on a Raycast hit, if the Raycast doesn't hit anything it returns the same vector</para>
    /// </summary>
    public static Vector3 RayHitPosition(this Vector3 vector , LayerMask layer , Vector3 direction, out bool hit)
    {
        Ray centerRay = new Ray(vector, direction);
        if (Physics.Raycast(centerRay, out RaycastHit hitInfo, 200f, layer))
        {
            Vector3 centerPos;
            centerPos.x = hitInfo.point.x;
            centerPos.y = hitInfo.point.y;
            centerPos.z = hitInfo.point.z;
            hit = true;
            return centerPos;
        }

        hit = false;
        return vector;
    }
    public static Vector3 RayHitPosition(this Vector3 vector , LayerMask layer , Vector3 direction)
    { 
        return RayHitPosition(vector, layer, direction, out bool ignore);
    }
}

public enum Axis {X,Y,Z}
