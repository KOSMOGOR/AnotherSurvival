using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "AnotherSurvival/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public float DamageBase { get; private set; } = 10f;
    [field: SerializeField] public float ReloadBase { get; private set; } = 1f;
    [field: SerializeField] public float AreaBase { get; private set; } = 1f;
    [field: SerializeField] public float AngleBase { get; private set; } = 90f;

    [field: SerializeField] public ProjectileInfo ProjectileInfo { get; private set; }
}
