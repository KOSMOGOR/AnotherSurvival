using UnityEngine;
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
        IWeapon weapon = _container.InstantiatePrefabForComponent<IWeapon>(weaponInfo.weaponPrefab);
        weapon.WeaponConfig = weaponInfo.weaponConfig;
        return weapon;
    }

    public class Factory : PlaceholderFactory<WeaponInfo, IWeapon> {}
}
