using UnityEngine;

public class ProjectileLaser : BaseProjectile
{
    [SerializeField] Timer damageCooldownTime = new(0.5f);
    [SerializeField] float laserDistance = 10f;
    [SerializeField] float growthSpeedBase = 1f;

    protected override void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.zero;
        damageCooldownTime.Update(Time.fixedDeltaTime);
        if (damageCooldownTime.IsCompleteAndReset())
        {
            foreach (RaycastHit2D raycastHit in Physics2D.CircleCastAll(Position, _projectileParams.area / 2f, Direction, laserDistance))
            {
                if (PointInLaser(Position - raycastHit.point, Direction) && raycastHit.collider.TryGetComponent(out IEnemy enemy)) AffectEnemy(enemy);
            }
        }
    }

    bool PointInLaser(Vector2 point, Vector2 direction)
    {
        direction.ProjectionLenFrom(point, out float projectionLen, out float perpendicularLen);
        float maxPossibleDistance = Mathf.Min(projectionLen * EvaluateGrowthSpeed(), _projectileParams.area / 2f);
        return perpendicularLen <= maxPossibleDistance;
    }

    float EvaluateGrowthSpeed() => growthSpeedBase;
}
