using UnityEngine;

public static class GroundInfo
{
    /// <summary>
    ///     <para>Returns the grounded position on a Raycast hit, returns a Vector3.Zero if the Raycast doesnt hit anything</para>
    /// </summary>
    /// <param name="point"></param>
    /// <param name="groundLayer"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 FloorReference(Vector3 point , LayerMask groundLayer , Vector3 direction)
    {
        Ray centerRay = new Ray(point, direction);
        if (Physics.Raycast(centerRay, out RaycastHit hitInfo, 200f, groundLayer))
        {
            Vector3 centerPos;
            centerPos.x = (int)hitInfo.point.x;
            centerPos.y = (int)hitInfo.point.y;
            centerPos.z = 0;
            
            return centerPos;
        }
        return Vector3.zero;
    }
    
    public static Vector3 NavigatingPosition(Vector3 input, Vector3 currentPos, LayerMask groundLayer)
    {
        Vector3 groundPos = PlatformReference(currentPos, groundLayer, input);
        Vector3 desiredPos;
        desiredPos.x = (int)currentPos.x + (int)input.x;
        desiredPos.y = groundPos.y;
        desiredPos.z = 0;
        
        return desiredPos;
    }

    public static Vector3 PlatformReference(Vector3 point, LayerMask groundLayer, Vector3 direction)
    {
        Vector3 centerPos = Vector3.zero;
        Vector3 rayDirection = Vector3.zero;
        rayDirection.y = direction.y > 0 ? 1 : -1;
        Ray centerRay = new Ray(point, rayDirection);
        RaycastHit[] hits = Physics.RaycastAll(centerRay, 200f, groundLayer);
        if (hits.Length > 0)
        {
            if (direction.y > -.1f)
            {
                centerPos = hits[0].point;
            }
            else
            {
                centerPos = hits[1].point;
            }
        }
        return centerPos;
    }
    
}