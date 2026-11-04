using UnityEngine;

public interface IAttackPattern
{
    ProjectileInfo ProjectileInfo { get; set; }

    void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams);
}
