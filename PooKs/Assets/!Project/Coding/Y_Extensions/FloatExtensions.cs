using UnityEngine;

public static class FloatExtensions
{
    /// <summary>
    ///  <para>Multiplies the value with a given speed and delta time</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static float SpeedWithDeltaTime(this float value, float speed)
    {
        float multipliedFloat;
        multipliedFloat = value * speed * Time.deltaTime;
        return multipliedFloat;
    }
}
