using UnityEngine;
using Zenject;

public class EnemyTargetGetter
{
    [Inject] readonly Player _player;

    public Vector2 GetTarget() => _player.Position;
}
