using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "AnotherSurvival/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public BaseWeapon weaponPrefab;
    public WeaponConfig weaponConfig;
}
