using UnityEngine;
using Zenject;

public class BaseAttackPattern : MonoBehaviour, IAttackPattern
{
    [Inject] readonly ProjectileFactory.Factory _projectileFactory;

    public ProjectileInfo ProjectileInfo { get; set; }

    public void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        IProjectile projectile = _projectileFactory.Create(ProjectileInfo, projectileParams);
        projectile.Position = position;
        projectile.SetDirection(direction);
    }
}
