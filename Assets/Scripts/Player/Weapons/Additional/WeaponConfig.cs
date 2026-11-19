using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "AnotherSurvival/Weapons/WeaponConfig", order = -100)]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public bool IsStarting { get; private set; } = false;

    [field: SerializeField] public float DamageBase { get; private set; } = 10f;
    [field: SerializeField] public float ReloadBase { get; private set; } = 1f;
    [field: SerializeField] public float AreaBase { get; private set; } = 1f;
    [field: SerializeField] public float AngleBase { get; private set; } = 90f;
    [field: SerializeField, SerializedDictionary("Stat Name", "Base Stat Value")] public SerializedDictionary<string, float> AdditionalStats { get; private set; } = new();

    [field: SerializeField] public List<LevelConfig> LevelConfigs { get; private set; } = new();

    [field: SerializeField] public ProjectileInfo ProjectileInfo { get; private set; }

    [Serializable]
    public class LevelConfig
    {
        [field: SerializeField] public string AdditionalDescription { get; private set; }
        public string Description => AdditionalDescription; // TODO: implement proper description

        [field: SerializeField] public float DamageDelta { get; private set; }
        [field: SerializeField] public float ReloadDelta { get; private set; }
        [field: SerializeField] public float AreaDelta { get; private set; }
        [field: SerializeField] public float AngleDelta { get; private set; }
        [field: SerializeField, SerializedDictionary("Stat Name", "Stat Delta")] public SerializedDictionary<string, float> AdditionalStats { get; private set; } = new();
    }
}
