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
    public Vector2 Direction { get; private set; } = Vector2.right;
    public Vector2 Velocity => Direction * Speed;

    [Inject] protected readonly ProjectileParams _projectileParams;
    readonly HashSet<IEnemy> hittedEnemies = new();

    protected Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        _rb.linearVelocity = Velocity;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (Penetration > 0 && hittedEnemies.Count >= Penetration) return;
        if (collider.TryGetComponent(out IEnemy enemy) && !hittedEnemies.Contains(enemy))
        {
            AffectEnemy(enemy);
            hittedEnemies.Add(enemy);
            if (Penetration > 0 && hittedEnemies.Count >= Penetration) Remove();
        }
    }

    protected virtual void AffectEnemy(IEnemy enemy) => enemy.TakeDamage(_projectileParams.damage);

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 dir) => Direction = dir.normalized;
}

public class ProjectileParams
{
    public float damage;
    public float area;
    public float angle;
}
