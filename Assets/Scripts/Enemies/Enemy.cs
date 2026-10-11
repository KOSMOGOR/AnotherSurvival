using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float canBeOutOfZone = 5f;

    EnemySettings _settings;
    [Inject] readonly EnemyGoodZone _enemyGoodZone;
    [Inject] readonly EnemyTargetGetter _enemyTargetGetter;
    Rigidbody2D _rb;

    float outOfZone = 0f;

    public Action<Enemy> onDeath;

    [Inject]
    public void Contstruct(EnemySettings settings)
    {
        _settings = settings;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

    public bool IsTooLongOutOfZone => outOfZone >= canBeOutOfZone;

    [Serializable]
    public class EnemySettings
    {
        public float speed = 1f;
    }

    public class Factory : PlaceholderFactory<Enemy> {}
}
