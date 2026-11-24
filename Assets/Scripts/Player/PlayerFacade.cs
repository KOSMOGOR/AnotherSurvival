using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    public Player Player { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerWeapons PlayerWeapons { get; private set; }

    [Inject]
    public void Construct(Player player, PlayerMovement playerMovement, PlayerWeapons playerWeapons)
    {
        Player = player;
        PlayerMovement = playerMovement;
        PlayerWeapons = playerWeapons;
    }

    public void ProvideInput(PlayerInputInfo inputInfo) => PlayerMovement.ProvideInput(inputInfo);

    public Vector2 Position => Player.Position;
    public Vector2 Forward => PlayerMovement.Forward;

    public void AddOrUpgradeWeapon(WeaponInfo weaponInfo)
    {
        if (!PlayerWeapons.HasWeapon(weaponInfo)) PlayerWeapons.AddWeapon(weaponInfo);
        else PlayerWeapons.UpgradeWeapon(weaponInfo);
    }

    public void AddOrUpgradeItem(Item item)
    {
        if (!PlayerWeapons.HasItem(item)) PlayerWeapons.AddItem(item);
        else PlayerWeapons.UpgradeItem(item);
    }

    public float EvaluateDamage(BaseWeapon weapon) => PlayerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.DamageBase, (lc, stat) => stat + lc.DamageDelta, (pls, stat) => stat + pls.damage);
    public float EvaluateReload(BaseWeapon weapon) => PlayerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.ReloadBase, (lc, stat) => stat + lc.ReloadDelta, (pls, stat) => stat + pls.reload);
    public float EvaluateArea(BaseWeapon weapon) => PlayerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AreaBase, (lc, stat) => stat + lc.AreaDelta, (pls, stat) => stat + pls.area);
    public float EvaluateAngle(BaseWeapon weapon) => PlayerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AngleBase, (lc, stat) => stat + lc.AngleDelta, (pls, stat) => stat + pls.angle);
    public float EvaluateAdditionalStat(BaseWeapon weapon, string statName) => PlayerWeapons.EvaluateFullStat(weapon.WeaponConfig, wc => wc.AdditionalStats.GetValueOrDefault(statName, 0f), (lc, stat) => stat + lc.AdditionalStats.GetValueOrDefault(statName, 0f), null);
}
