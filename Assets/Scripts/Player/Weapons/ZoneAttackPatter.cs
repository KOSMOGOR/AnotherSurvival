using UnityEngine;

public class ZoneAttackPatter : BaseAttackPattern
{
    public override void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(position, projectileParams.area))
        {
            if (!collider.TryGetComponent(out IEnemy enemy)) return;
            if (Vector2.Angle(direction, enemy.Position - position) <= projectileParams.angle) enemy.TakeDamage(projectileParams.damage);
        }
    }
}
