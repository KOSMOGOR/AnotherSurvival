using UnityEngine;

public interface IEnemy : IDamageable
{
    Vector2 Position { get; }
}
