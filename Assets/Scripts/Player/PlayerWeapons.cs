using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerWeapons : MonoBehaviour
{
    [Inject] readonly WeaponFactory.Factory _weaponFactory;
    [Inject] readonly Player _player;

    readonly Dictionary<WeaponInfo, WeaponRuntimeInfo> weaponRuntimeInfos = new();
    readonly List<ItemRuntimeInfo> itemRuntimeInfos = new();

    void FixedUpdate()
    {
        foreach (WeaponRuntimeInfo wri in weaponRuntimeInfos.Values) wri.weaponInstance.OnUpdate(Time.fixedDeltaTime);
    }

    public bool HasWeapon(WeaponInfo weaponInfo) => weaponRuntimeInfos.ContainsKey(weaponInfo);

    public void AddWeapon(WeaponInfo weaponInfo)
    {
        BaseWeapon weapon = _weaponFactory.Create(weaponInfo) as BaseWeapon;
        weapon.transform.SetParent(transform);
        weaponRuntimeInfos.Add(weaponInfo, new()
        {
            // weaponInfo = weaponInfo,
            weaponInstance = weapon,
            level = 1
        });
    }

    public void UpgradeWeapon(WeaponInfo weaponInfo)
    {
        if (weaponRuntimeInfos.TryGetValue(weaponInfo, out WeaponRuntimeInfo wri)) wri.level += 1;
    }

    public void AddItem(Item item)
    {
        itemRuntimeInfos.Add(new()
        {
            item = item,
            level = 1
        });
        EvaluateHealthStats();
    }

    public void UpgradeItem(Item item)
    {
        ItemRuntimeInfo itemRuntimeInfo = itemRuntimeInfos.Find(iri => iri.item == item);
        if (itemRuntimeInfo != null) itemRuntimeInfo.level += 1;
        EvaluateHealthStats();
    }

    public float EvaluateWeaponStat(WeaponConfig weaponConfig,
        Func<WeaponConfig, float> baseStatGetter,
        Func<WeaponConfig.LevelConfig, float, float> statPerLevelApplier)
    {
        WeaponInfo weaponInfo = weaponRuntimeInfos.Keys.FirstOrDefault(weaponInfo => weaponInfo.weaponConfig == weaponConfig);
        if (!weaponInfo) return default;
        WeaponRuntimeInfo wri = weaponRuntimeInfos[weaponInfo];
        return EvaluateWeaponStat(weaponConfig, wri.level, baseStatGetter, statPerLevelApplier);
    }

    public float EvaluateWeaponStat(WeaponConfig weaponConfig, int level,
        Func<WeaponConfig, float> baseStatGetter,
        Func<WeaponConfig.LevelConfig, float, float> statPerLevelApplier)
    {
        float stat = baseStatGetter.Invoke(weaponConfig);
        for (int i = 0; i <= level - 1; i++)
            if (weaponConfig.LevelConfigs.TryGet(i, out WeaponConfig.LevelConfig levelConfig)) stat = statPerLevelApplier.Invoke(levelConfig, stat);
            else break;
        return stat;
    }

    public float EvaluateItemsStat(float baseStat, Func<Item.PerLevelStats, float, float> itemStatApplier)
    {
        foreach (ItemRuntimeInfo itemRuntimeInfo in itemRuntimeInfos)
            for (int i = 0; i < itemRuntimeInfo.level; i++) baseStat = itemStatApplier.Invoke(itemRuntimeInfo.item.LevelStats, baseStat);
        return baseStat;
    }

    public float EvaluateFullStat(WeaponConfig weaponConfig,
        Func<WeaponConfig, float> baseStatGetter,
        Func<WeaponConfig.LevelConfig, float, float> statPerLevelApplier,
        Func<Item.PerLevelStats, float, float> itemStatApplier)
    {
        float stat = EvaluateWeaponStat(weaponConfig, baseStatGetter, statPerLevelApplier);
        stat = EvaluateItemsStat(stat, itemStatApplier);
        return stat;
    }

    void EvaluateHealthStats()
    {
        float maxHp = EvaluateItemsStat(_player._playerStats.maxHp, (pls, stat) => stat + pls.maxHealth);
        float hpRegen = EvaluateItemsStat(_player._playerStats.hpRegen, (pls, stat) => stat + pls.healthRegen);
        _player.SetHealthStats(maxHp, hpRegen);
    }

    class WeaponRuntimeInfo
    {
        // public WeaponInfo weaponInfo;
        public BaseWeapon weaponInstance;
        public int level;
    }

    class ItemRuntimeInfo
    {
        public Item item;
        public int level;
    }
}
