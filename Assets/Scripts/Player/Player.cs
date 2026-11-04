using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public Vector3 Position => transform.position;

    [field: SerializeField] public float Hp { get; private set; }
    [field: SerializeField] public float MaxHp { get; private set; } = 100f;
    [field: SerializeField] public Timer InvularabilityAfterDamage { get; private set; } = new(1f);
    [field: SerializeField] public int CurrentLevel { get; private set; } = 1;
    [field: SerializeField] public float CurrentXp { get; private set; }

    [field: SerializeField] public bool IsDead { get; private set; } = false;

    public event Action<IDamageable, float> OnTakeDamage;
    public event Action<IDamageable> OnDeath;
    public event Action OnNewLevel;

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
}
