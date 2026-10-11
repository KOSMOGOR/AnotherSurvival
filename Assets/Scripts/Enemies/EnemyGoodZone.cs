using UnityEngine;
using Zenject;

public class EnemyGoodZone
{
    [Inject] readonly GameCamera _gameCamera;

    public Rect GoodZone => _gameCamera.CameraRectWorld;

    public bool IsInZone(Vector2 position) => GoodZone.Contains(position);
}
