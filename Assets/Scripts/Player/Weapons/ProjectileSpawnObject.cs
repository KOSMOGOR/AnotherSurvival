using UnityEngine;
using Zenject;

public class ProjectileSpawnObject : BaseProjectile
{
    [SerializeField] bool dealDamage;
    [SerializeField] ProjectileInfo objectToSpawn;

    [Inject] readonly ProjectileFactory.Factory _projectileFactory;

    protected override void AffectEnemy(IEnemy enemy)
    {
        if (dealDamage) base.AffectEnemy(enemy);
        IProjectile projectile = _projectileFactory.Create(objectToSpawn, _projectileParams);
        projectile.Position = Position;
    }
}
