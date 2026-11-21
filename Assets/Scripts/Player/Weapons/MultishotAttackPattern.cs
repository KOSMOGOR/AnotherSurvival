using Cysharp.Threading.Tasks;
using UnityEngine;

public class MultishotAttackPattern : BaseAttackPattern
{
    // [SerializeField] int projectileCountBase = 5;
    [SerializeField] float multishotAngle = 60f;
    [SerializeField] bool useProjectileAngle = false;
    [SerializeField] float delayBetweenShots = 0f;

    public override void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        AttackDirectionAsync(position, direction, projectileParams).Forget();
    }

    async UniTaskVoid AttackDirectionAsync(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        float angle = useProjectileAngle ? projectileParams.angle : multishotAngle;
        int projectileCount = (int)projectileParams.EvaluateStat(AdditionalStats.PROJECTILE_COUNT);
        if (projectileCount < 1) projectileCount = 1;
        float multishotOffsetPerI = angle / (projectileCount - 1);
        float multishotStartAngle = -angle / 2f;
        for (int i = 0; i < projectileCount; i++)
        {
            destroyCancellationToken.ThrowIfCancellationRequested();
            Vector2 thisDirection = direction.RotatedBy(multishotStartAngle + multishotOffsetPerI * i);
            SpawnProjectileWithDirection(position, thisDirection, projectileParams);
            if (delayBetweenShots > 0) await UniTask.WaitForSeconds(delayBetweenShots);
        }
    }
}
