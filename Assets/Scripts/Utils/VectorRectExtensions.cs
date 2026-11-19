using UnityEngine;

public static class VectorRectExtensions
{
#region Rect

    public static Rect With(this Rect rect, float? x = null, float? y = null, float? w = null, float? h = null) =>
        new(x ?? rect.x, y ?? rect.y, w ?? rect.width, h ?? rect.height);

    public static Rect Add(this Rect rect, float? x = null, float? y = null, float? w = null, float? h = null) =>
        new(rect.x + x ?? 0, rect.y + y ?? 0, rect.width + w ?? 0, rect.height + h ?? 0);

    public static Vector2 GetRandomPointInside(this Rect rect) => new(rect.x + Random.Range(0, rect.width), rect.y + Random.Range(0, rect.height));

    public static float Area(this Rect rect) => Mathf.Abs(rect.width * rect.height);

#endregion
#region Vector2

    public static Vector2 With(this Vector2 vec, float? x = null, float? y = null) => new(x ?? vec.x, y ?? vec.y);

    public static Vector2 Add(this Vector2 vec, float? x = null, float? y = null) => new(vec.x + x ?? 0, vec.y + y ?? 0);

    public static Vector2 RotatedBy(this Vector2 vec, float angle) => Quaternion.Euler(0, 0, angle) * vec;

    public static Vector2 WithLength(this Vector2 vec, float length) => vec.normalized * length;

    public static void ProjectionLenFrom(this Vector2 vec, Vector2 point, out float projectionLen, out float perpendicularLen)
    {
        projectionLen = Vector2.Dot(vec, point) / vec.magnitude;
        perpendicularLen = Mathf.Sqrt(point.sqrMagnitude - projectionLen * projectionLen);
    }

    public static void ProjectionFrom(this Vector2 vec, Vector2 point, out Vector2 projection, out Vector2 perpendicular)
    {
        vec.ProjectionLenFrom(point, out float projectionLen, out float _);
        projection = vec.WithLength(projectionLen);
        perpendicular = projection - point;
    }

#endregion
#region Vector3

    public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null) => new(x ?? vec.x, y ?? vec.y, z ?? vec.z);

    public static Vector3 WithLength(this Vector3 vec, float length) => vec.normalized * length;

#endregion
}
