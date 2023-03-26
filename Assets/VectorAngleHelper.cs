using UnityEngine;

public static class VectorAngleHelper
{
    public static Vector2 AngleToVector2(float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
