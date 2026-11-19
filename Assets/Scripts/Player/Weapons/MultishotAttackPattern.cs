using Cysharp.Threading.Tasks;
using UnityEngine;

public class MultishotAttackPattern : BaseAttackPattern
{
    [SerializeField] int multishotCount = 5;
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
        float multishotOffsetPerI = angle / (multishotCount - 1);
        float multishotStartAngle = -angle / 2f;
        for (int i = 0; i < multishotCount; i++)
        {
            destroyCancellationToken.ThrowIfCancellationRequested();
            Vector2 thisDirection = direction.RotatedBy(multishotStartAngle + multishotOffsetPerI * i);
            SpawnProjectileWithDirection(position, thisDirection, projectileParams);
            if (delayBetweenShots > 0) await UniTask.WaitForSeconds(delayBetweenShots);
        }
    }
}
