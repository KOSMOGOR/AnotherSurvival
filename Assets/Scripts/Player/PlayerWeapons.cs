using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerWeapons : MonoBehaviour
{
    [Inject] readonly WeaponFactory.Factory _weaponFactory;

    readonly List<WeaponRuntimeInfo> weaponRuntimeInfos = new();

    void FixedUpdate()
    {
        foreach (WeaponRuntimeInfo wri in weaponRuntimeInfos) wri.weaponInstance.OnUpdate(Time.fixedDeltaTime);
    }

    public bool HasWeapon(WeaponInfo weaponInfo) => weaponRuntimeInfos.Any(wri => wri.weaponInfo == weaponInfo);

    public void AddWeapon(WeaponInfo weaponInfo)
    {
        BaseWeapon weapon = _weaponFactory.Create(weaponInfo) as BaseWeapon;
        weapon.transform.SetParent(transform);
        weaponRuntimeInfos.Add(new()
        {
            weaponInfo = weaponInfo,
            weaponInstance = weapon,
            level = 1
        });
    }

    public void UpgradeWeapon(WeaponInfo weaponInfo)
    {
        if (!HasWeapon(weaponInfo)) return;
        WeaponRuntimeInfo weaponRuntimeInfo = weaponRuntimeInfos.First(wri => wri.weaponInfo == weaponInfo);
        weaponRuntimeInfo.level += 1;
    }

    struct WeaponRuntimeInfo
    {
        public WeaponInfo weaponInfo;
        public BaseWeapon weaponInstance;
        public int level;
    }
}
