using UnityEngine;

public class ProjectileLaser : BaseProjectile
{
    [SerializeField] Timer damageCooldownTime = new(0.5f);
    [SerializeField] float laserDistance = 10f;

    protected override void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.zero;
        damageCooldownTime.Update(Time.fixedDeltaTime);
        if (damageCooldownTime.IsCompleteAndReset())
        {
            foreach (RaycastHit2D raycastHit in Physics2D.CircleCastAll(Position, _projectileParams.area, Direction, laserDistance))
            {
                if (raycastHit.collider.TryGetComponent(out IEnemy enemy)) AffectEnemy(enemy);
            }
        }
    }
}
