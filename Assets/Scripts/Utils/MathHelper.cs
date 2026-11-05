using UnityEngine;

public static class MathHelper
{
    public static int DirectionToClock(Vector2 direcion, int clockCount)
    {
        Vector2 vec = direcion.RotatedBy(-45f);
        float angle = -Mathf.Atan2(vec.y, vec.x) / Mathf.PI + 2f; // 1 - 3
        if (angle >= 2) angle -= 2; // 0 - 2
        angle *= clockCount / 2f; // 0 - clockCount
        return Mathf.RoundToInt(angle);
    }
}