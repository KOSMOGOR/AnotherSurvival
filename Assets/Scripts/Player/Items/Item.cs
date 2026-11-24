using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "AnotherSurvival/Item", order = -100)]
public class Item : ScriptableObject
{
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public PerLevelStats BaseStats { get; private set; }
    [field: SerializeField] public PerLevelStats DeltaStats { get; private set; }

    [Serializable]
    public struct PerLevelStats
    {
        public float maxHealth;
        public float healthRegen;
        public float damage;
        public float reload;
        public float area;
        public float angle;
        public float xp;
    }
}
