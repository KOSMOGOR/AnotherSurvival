using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TargetInAngleWeapon : BaseWeapon
{
    [Tooltip("Offsets from player's forward direction")]
    [field: SerializeField] public float[] AttackAngles { get; private set; } = { 0 };
    [field: SerializeField] public bool RequireTargets { get; private set; } = false;

    protected HashSet<IEnemy> potentialTargets = new();
    protected List<IEnemy> targets = new();

    protected override void Start()
    {
        base.Start();
        _attackPattern.ProjectileInfo = WeaponConfig.ProjectileInfo;
    }

    public override void OnUpdate(float deltaTime)
    {
        if (RequireTargets) GatherTargets();
        base.OnUpdate(deltaTime);
    }

    protected override bool CanAttack() => !RequireTargets || targets.Count > 0;

    protected override void Attack()
    {
        ProjectileParams projectileParams = new()
        {
            damage = WeaponConfig.DamageBase
        };
        if (RequireTargets)
        {
            Vector2 closestPos = targets.Select(enemy => PosToDir(enemy.Position)).OrderBy(x => x.sqrMagnitude).First();
            AttackDirection(closestPos, projectileParams);
        }
        else
            foreach (float angle in AttackAngles)
            {
                Vector2 dir = _playerFacade.Forward.RotateBy(angle);
                AttackDirection(dir, projectileParams);
            }
    }

    protected virtual void AttackDirection(Vector2 direction, ProjectileParams projectileParams)
    {
        _attackPattern.AttackDirection(_playerFacade.Position, direction, projectileParams);
    }

    void GatherTargets()
    {
        targets.Clear();
        foreach (IEnemy enemy in potentialTargets)
        {
            float enemyAngleFromForward = Vector2.SignedAngle(_playerFacade.Forward, enemy.Position - _playerFacade.Position);
            if (AttackAngles.Any(attackAngle => Mathf.Abs(attackAngle - enemyAngleFromForward) <= WeaponConfig.AngleBase))
                targets.Add(enemy);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out IEnemy enemy)) potentialTargets.Add(enemy);
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out IEnemy enemy)) potentialTargets.Remove(enemy);
    }
}
