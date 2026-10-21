using System;

public interface IDamageable
{
    public float Hp { get; }
    public float MaxHp { get; }

    public void TakeDamage(float damage);
    public void Die();
    public void Remove();

    public event Action<IDamageable, float> OnTakeDamage;
    public event Action<IDamageable> OnDeath;
}
