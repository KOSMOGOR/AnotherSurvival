using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    Camera _camera;

    public Rect CameraRectWorld
    {
        get
        {
            float halfHeight = _camera.orthographicSize;
            float halfWidth = halfHeight * _camera.aspect;
            Vector2 halfWidthHeight = new(halfWidth, halfHeight);
            return new((Vector2)transform.position - halfWidthHeight, halfWidthHeight * 2);
        }
    }

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }
}
