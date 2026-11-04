using Zenject;

public class ProjectileFactory : IFactory<ProjectileInfo, ProjectileParams, IProjectile>
{
    readonly DiContainer _container;

    public ProjectileFactory(DiContainer container)
    {
        _container = container;
    }

    public IProjectile Create(ProjectileInfo projectileInfo, ProjectileParams projectileParams)
    {
        return _container.InstantiatePrefabForComponent<BaseProjectile>(projectileInfo.projectilePrefab, new[] { projectileParams });
    }

    public class Factory : PlaceholderFactory<ProjectileInfo, ProjectileParams, IProjectile> {}
}
