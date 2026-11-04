using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[RequireComponent(typeof(Rigidbody2D))]
public class BaseProjectile : MonoBehaviour, IProjectile
{
    [field: SerializeField] public float TimeToLive { get; private set; } = 3f;
    [field: SerializeField] public float Speed { get; private set; } = 2f;
    [field: SerializeField] public int Penetration { get; private set; } = 1;

    public Vector2 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    public Vector2 Velocity { get; private set; } = Vector2.right;

    [Inject] readonly ProjectileParams _projectileParams;
    readonly HashSet<IEnemy> hittedEnemies = new();

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = Velocity;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (hittedEnemies.Count >= Penetration) return;
        if (collider.TryGetComponent(out IEnemy enemy) && !hittedEnemies.Contains(enemy))
        {
            enemy.TakeDamage(_projectileParams.damage);
            hittedEnemies.Add(enemy);
            if (Penetration > 0 && hittedEnemies.Count >= Penetration) Remove();
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 dir)
    {
        Velocity = dir * Speed;
    }
}

public class ProjectileParams
{
    public float damage;
}
