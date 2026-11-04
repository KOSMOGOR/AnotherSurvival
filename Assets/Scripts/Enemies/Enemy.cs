using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] float canBeOutOfZone = 5f;

    EnemySettings _settings;
    [Inject] readonly EnemyGoodZone _enemyGoodZone;
    [Inject] readonly EnemyTargetGetter _enemyTargetGetter;
    Rigidbody2D _rb;

    float outOfZone = 0f;

    public event Action<IDamageable, float> OnTakeDamage;
    public event Action<IDamageable> OnDeath;

    public Vector2 Position => transform.position;
    [field: SerializeField] public float MaxHp { get; private set; } = 20f;
    [field: SerializeField] public float Hp { get; private set; }
    public bool IsTooLongOutOfZone => outOfZone >= canBeOutOfZone;

    [Inject]
    public void Contstruct(EnemySettings settings)
    {
        _settings = settings;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Hp = MaxHp;
    }

    void FixedUpdate()
    {
        CheckEnemyInZone();
        Move();
    }

    void CheckEnemyInZone()
    {
        if (!_enemyGoodZone.IsInZone(transform.position)) outOfZone += Time.fixedDeltaTime;
        else outOfZone = 0f;
    }

    void Move()
    {
        Vector2 target = _enemyTargetGetter.GetTarget();
        Vector2 toTarget = target - (Vector2)transform.position;
        float maxSpeedDt = _settings.speed * Time.fixedDeltaTime;
        if (toTarget.magnitude > maxSpeedDt) toTarget = toTarget.normalized * maxSpeedDt;
        _rb.linearVelocity = toTarget / Time.fixedDeltaTime;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        OnTakeDamage?.Invoke(this, damage);
        if (Hp <= 0) Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
        Remove();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    [Serializable]
    public class EnemySettings
    {
        public float speed = 1f;
    }

    public class Factory : PlaceholderFactory<Enemy> {}
}
