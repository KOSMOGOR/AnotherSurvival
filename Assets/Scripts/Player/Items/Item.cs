using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "AnotherSurvival/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public PerLevelStats LevelStats { get; private set; }

    public struct PerLevelStats
    {
        public float maxHealth;
        public float healthRegen;
        public float damage;
        public float reloadPercent;
        public float area;
        public float angle;
        public float xp;
    }
}
