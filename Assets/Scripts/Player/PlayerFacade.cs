using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    Player _player;
    PlayerMovement _playerMovement;
    PlayerWeapons _playerWeapons;

    [Inject]
    public void Construct(Player player, PlayerMovement playerMovement, PlayerWeapons playerWeapons)
    {
        _player = player;
        _playerMovement = playerMovement;
        _playerWeapons = playerWeapons;
    }

    public void ProvideInput(PlayerInputInfo inputInfo) => _playerMovement.ProvideInput(inputInfo);

    public Vector2 Position => _player.Position;
    public Vector2 Forward => _playerMovement.Forward;

    public void AddOrUpgradeWeapon(WeaponInfo weaponInfo)
    {
        if (!_playerWeapons.HasWeapon(weaponInfo)) _playerWeapons.AddWeapon(weaponInfo);
        else _playerWeapons.UpgradeWeapon(weaponInfo);
    }

    public void AddOrUpgradeItem(Item item)
    {
        if (!_playerWeapons.HasItem(item)) _playerWeapons.AddItem(item);
        else _playerWeapons.UpgradeItem(item);
    }

    public float EvaluateDamage(BaseWeapon weapon) => _playerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.DamageBase, (lc, stat) => stat + lc.DamageDelta, (pls, stat) => stat + pls.damage);
    public float EvaluateReload(BaseWeapon weapon) => _playerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.ReloadBase,
        (lc, stat) => lc.ReloadDelta > 0f ? stat * lc.ReloadDelta : stat,
        (pls, stat) => pls.reloadPercent > 0f ? stat + pls.reloadPercent : stat);
    public float EvaluateArea(BaseWeapon weapon) => _playerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AreaBase, (lc, stat) => stat + lc.AreaDelta, (pls, stat) => stat + pls.area);
    public float EvaluateAngle(BaseWeapon weapon) => _playerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AngleBase, (lc, stat) => stat + lc.AngleDelta, (pls, stat) => stat + pls.angle);
    public float EvaluateAdditionalStat(BaseWeapon weapon, string statName) => _playerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AdditionalStats.GetValueOrDefault(statName, 0f), (lc, stat) => stat + lc.AdditionalStats.GetValueOrDefault(statName, 0f), null);
}
