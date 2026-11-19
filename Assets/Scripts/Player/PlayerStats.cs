using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "AnotherSurvival/PlayerStats", order = -100)]
public class PlayerStats : ScriptableObject
{
    public float maxHp = 100f;
    public float hpRegen = 1f;
}
