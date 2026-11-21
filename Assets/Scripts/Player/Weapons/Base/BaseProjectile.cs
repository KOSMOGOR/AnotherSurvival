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
    [SerializeField] bool fixedSize = false;

    public Vector2 Position
    {
        get { return transform.position; }
        set
        {
            transform.position = value;
            _rb.position = value;
        }
    }
    public Vector2 Direction { get; private set; } = Vector2.right;
    public Vector2 Velocity => Direction * Speed;

    protected ProjectileParams _projectileParams;
    readonly HashSet<IEnemy> hittedEnemies = new();
    protected float timeExists = 0f;

    protected Rigidbody2D _rb;

    [Inject]
    public void Construct(ProjectileParams projectileParams)
    {
        _projectileParams = projectileParams;
        if (!fixedSize) transform.localScale = new(projectileParams.area, projectileParams.area, projectileParams.area);
        int penetration = (int)projectileParams.EvaluateStat(AdditionalStats.PENETRATION);
        if (Penetration != 0) Penetration = penetration;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start() {}

    protected virtual void Update()
    {
        if (timeExists <= TimeToLive) timeExists = Mathf.Min(timeExists + Time.deltaTime, TimeToLive);
        if (TimeToLiveExpired) Remove();
    }

    public virtual bool TimeToLiveExpired => timeExists >= TimeToLive;

    protected virtual void FixedUpdate()
    {
        _rb.linearVelocity = CurrentVelocity;
    }

    protected virtual Vector2 CurrentVelocity => Velocity;

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

    public Dictionary<string, Func<float, float>> additionalStats = new();

    public ProjectileParams WithStat(string statName, Func<float, float> statApplier)
    {
        additionalStats[statName] = statApplier;
        return this;
    }

    public ProjectileParams WithAddStat(string statName, float addToStat)
    {
        return WithStat(statName, stat => stat + addToStat);
    }

    public float EvaluateStat(string statName) => EvaluateStat(statName, 0f);
    public float EvaluateStat(string statName, float stat)
    {
        if (additionalStats.TryGetValue(statName, out Func<float, float> statApplier)) return statApplier.Invoke(stat);
        return stat;
    }
}
