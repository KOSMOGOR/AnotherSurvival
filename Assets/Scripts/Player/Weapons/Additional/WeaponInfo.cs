using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "AnotherSurvival/Weapons/WeaponInfo", order = -100)]
public class WeaponInfo : ScriptableObject
{
    public BaseWeapon weaponPrefab;
    public WeaponConfig weaponConfig;
}
