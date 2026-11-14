using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IDamageable
{
    public Vector3 Position => transform.position;

    [field: SerializeField] public float Hp { get; private set; }
    [field: SerializeField] public float MaxHp { get; private set; } = 100f;
    [field: SerializeField] public float HpRegen { get; private set; } = 1f;
    [field: SerializeField] public Timer InvularabilityAfterDamage { get; private set; } = new(1f);
    [field: SerializeField] public int CurrentLevel { get; private set; } = 1;
    [field: SerializeField] public float CurrentXp { get; private set; }

    [field: SerializeField] public bool IsDead { get; private set; } = false;

    public event Action<IDamageable, float> OnTakeDamage;
    public event Action<IDamageable> OnDeath;
    public event Action OnNewLevel;

    public PlayerStats _playerStats { get; private set; }

    [Inject]
    public void Construct(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        MaxHp = _playerStats.maxHp;
        Hp = MaxHp;
        HpRegen = _playerStats.hpRegen;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        OnTakeDamage?.Invoke(this, damage);
        if (Hp <= 0) Die();
    }

    public void Die()
    {
        IsDead = true;
        OnDeath?.Invoke(this);
    }

    public void Remove() {}

    public void AddXp(float xp)
    {
        CurrentXp += xp;
        CheckForNewLevel();
    }

    void CheckForNewLevel()
    {
        float xpForCurrentLevel = CurrentLevel * 100f;
        if (CurrentXp >= xpForCurrentLevel)
        {
            CurrentLevel += 1;
            CurrentXp -= xpForCurrentLevel;
            OnNewLevel?.Invoke();
        }
    }

    public void SetHealthStats(float maxHp, float hpRegen)
    {
        float maxHpDiff = Mathf.Max(MaxHp - maxHp, 0f);
        MaxHp = maxHp;
        Hp += maxHpDiff;
        HpRegen = hpRegen;
    }
}
