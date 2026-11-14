using Zenject;

public class WeaponFactory : IFactory<WeaponInfo, IWeapon>
{
    readonly DiContainer _container;

    public WeaponFactory(DiContainer container)
    {
        _container = container;
    }

    public IWeapon Create(WeaponInfo weaponInfo)
    {
        return _container.InstantiatePrefabForComponent<BaseWeapon>(weaponInfo.weaponPrefab, new[] { weaponInfo.weaponConfig });
    }

    public class Factory : PlaceholderFactory<WeaponInfo, IWeapon> {}
}
