using Cysharp.Threading.Tasks;
using UnityEngine;

public class SwingAttackPattern : BaseAttackPattern
{
    [SerializeField] float swingTime = 1f;
    [SerializeField] float swingOffset = 1f;

    public override void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        AttackDirectionAsync(position, direction, projectileParams).Forget();
    }

    async UniTaskVoid AttackDirectionAsync(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        float startAngle = -projectileParams.angle / 2f;
        IProjectile projectile = SpawnProjectileWithDirection(position, direction, projectileParams);
        float timePassed = 0f;
        while (timePassed <= swingTime && projectile != null)
        {
            destroyCancellationToken.ThrowIfCancellationRequested();
            SetProjectilePosition(projectile, position, direction, startAngle + projectileParams.angle * (timePassed / swingTime));
            await UniTask.WaitForFixedUpdate();
            timePassed += Time.fixedDeltaTime;
        }
    }

    void SetProjectilePosition(IProjectile projectile, Vector2 position, Vector2 direction, float angle)
    {
        projectile.Position = position + direction.RotatedBy(angle).WithLength(swingOffset);
    }
}
