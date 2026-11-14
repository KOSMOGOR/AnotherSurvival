using UnityEngine;
using Zenject;

public class BaseAttackPattern : MonoBehaviour, IAttackPattern
{
    [Inject] readonly ProjectileFactory.Factory _projectileFactory;

    public ProjectileInfo ProjectileInfo { get; set; }

    public virtual void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        SpawnProjectileWithDirection(position, direction, projectileParams);
    }

    protected virtual void SpawnProjectileWithDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        IProjectile projectile = _projectileFactory.Create(ProjectileInfo, projectileParams);
        projectile.Position = position;
        projectile.SetDirection(direction);
    }
}
