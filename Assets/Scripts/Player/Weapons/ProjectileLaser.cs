using UnityEngine;

public class ProjectileLaser : BaseProjectile
{
    [SerializeField] Timer damageCooldownTime = new(0.5f);
    [SerializeField] float laserDistance = 10f;
    [SerializeField] float growthSpeedBase = 1f;

    static readonly float[] laserAngles = new float[] { 0f, 180, 90f, -90f };

    int laserCount;

    protected override void Start()
    {
        base.Start();
        laserCount = (int)_projectileParams.EvaluateStat(AdditionalStats.LASER_COUNT);
        laserCount = Mathf.Clamp(laserCount, 1, laserAngles.Length);
    }

    protected override void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.zero;
        damageCooldownTime.Update(Time.fixedDeltaTime);
        if (damageCooldownTime.IsCompleteAndReset())
        {
            for (int i = 0; i < laserCount; i++) CastLaserInDirection(Direction.RotatedBy(laserAngles[i]));
        }
    }

    void CastLaserInDirection(Vector2 direction)
    {
        foreach (RaycastHit2D raycastHit in Physics2D.CircleCastAll(Position, _projectileParams.area / 2f, direction, laserDistance))
        {
            if (IsPointInLaser(Position - raycastHit.point, Direction) && raycastHit.collider.TryGetComponent(out IEnemy enemy)) AffectEnemy(enemy);
        }
    }

    bool IsPointInLaser(Vector2 point, Vector2 direction)
    {
        direction.ProjectionLenFrom(point, out float projectionLen, out float perpendicularLen);
        float maxPossibleDistance = Mathf.Min(projectionLen * EvaluateGrowthSpeed(), _projectileParams.area / 2f);
        return perpendicularLen <= maxPossibleDistance;
    }

    float EvaluateGrowthSpeed() => growthSpeedBase;
}
