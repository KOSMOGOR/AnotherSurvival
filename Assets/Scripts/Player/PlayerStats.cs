using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "AnotherSurvival/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float maxHp = 100f;
    public float hpRegen = 1f;
}
