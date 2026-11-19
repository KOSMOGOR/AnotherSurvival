using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetInAngleWeapon : BaseWeapon
{
    [Tooltip("Offsets from player's forward direction")]
    [field: SerializeField] public float[] AttackAngles { get; private set; } = { 0 };
    [field: SerializeField] public AttackTypeEnum AttackType { get; private set; } = AttackTypeEnum.AttackDirectlyEnemy;

    bool RequireTargets => AttackType != AttackTypeEnum.AttackAllAngles;

    protected HashSet<IEnemy> potentialTargets = new();
    protected List<List<IEnemy>> targetsInAngle = new();
    int TargetsCount => targetsInAngle.Sum(t => t.Count);

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

    protected override bool CanAttack() => !RequireTargets || TargetsCount > 0;

    protected override void Attack()
    {
        ProjectileParams projectileParams = new()
        {
            damage = _playerFacade.EvaluateDamage(this),
            area = _playerFacade.EvaluateArea(this),
            angle = _playerFacade.EvaluateAngle(this)
        };
        if (AttackType == AttackTypeEnum.AttackDirectlyEnemy)
        {
            for (int i = 0; i < AttackAngles.Length; i++)
                if (targetsInAngle[i].Count > 0)
                {
                    Vector2 closestPos = targetsInAngle[i].Select(enemy => PosToDir(enemy.Position)).OrderBy(x => x.sqrMagnitude).First();
                    AttackDirection(closestPos, projectileParams);
                }
        }
        else foreach (float angle in AttackAngles)
            {
                Vector2 dir = _playerFacade.Forward.RotatedBy(angle);
                AttackDirection(dir, projectileParams);
            }
    }

    protected virtual void AttackDirection(Vector2 direction, ProjectileParams projectileParams)
    {
        _attackPattern.AttackDirection(_playerFacade.Position, direction, projectileParams);
    }

    void GatherTargets()
    {
        EnsureLength(targetsInAngle, AttackAngles.Length);
        foreach (List<IEnemy> angleTargets in targetsInAngle) angleTargets.Clear();
        float angle = _playerFacade.EvaluateAngle(this);
        foreach (IEnemy enemy in potentialTargets)
        {
            float enemyAngleFromForward = Vector2.SignedAngle(_playerFacade.Forward, enemy.Position - _playerFacade.Position);
            for (int i = 0; i < AttackAngles.Length; i++)
                if (Mathf.Abs(AttackAngles[i] - enemyAngleFromForward) <= angle) targetsInAngle[i].Add(enemy);
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

    void EnsureLength<T>(List<List<T>> values, int length)
    {
        if (values.Count > length) values.RemoveRange(length, values.Count - length);
        while (values.Count < length) values.Add(new());
    }

    public enum AttackTypeEnum
    {
        AttackAllAngles,
        AttackAllAnglesIfHasEnemy,
        AttackDirectlyEnemy
    }
}
