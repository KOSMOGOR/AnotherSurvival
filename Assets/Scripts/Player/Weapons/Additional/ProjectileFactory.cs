using UnityEngine;
using Zenject;

public class ProjectileFactory : IFactory<ProjectileInfo, ProjectileParams, IProjectile>
{
    Transform parentTransform;

    readonly DiContainer _container;

    public ProjectileFactory(DiContainer container)
    {
        _container = container;
    }

    public IProjectile Create(ProjectileInfo projectileInfo, ProjectileParams projectileParams)
    {
        return _container.InstantiatePrefabForComponent<BaseProjectile>(projectileInfo.projectilePrefab, GetOrCreateParentTransform(), new[] { projectileParams });
    }

    Transform GetOrCreateParentTransform()
    {
        if (parentTransform == null) parentTransform = new GameObject("Projectiles").transform;
        return parentTransform;
    }

    public class Factory : PlaceholderFactory<ProjectileInfo, ProjectileParams, IProjectile> {}
}
