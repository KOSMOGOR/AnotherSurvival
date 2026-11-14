using Cysharp.Threading.Tasks;
using UnityEngine;

public class MultishotAttackPattern : BaseAttackPattern
{
    [SerializeField] int multishotCount = 5;
    [SerializeField] float multishotStartOffsetAngle = 30f;
    [SerializeField] float delayBetweenShots = 0f;

    public override void AttackDirection(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        AttackDirectionAsync(position, direction, projectileParams).Forget();
    }

    async UniTaskVoid AttackDirectionAsync(Vector2 position, Vector2 direction, ProjectileParams projectileParams)
    {
        float multishotOffsetPerI = multishotStartOffsetAngle * 2f / multishotCount;
        for (int i = 0; i < multishotCount; i++)
        {
            destroyCancellationToken.ThrowIfCancellationRequested();
            Vector2 thisDirection = direction.RotatedBy(-multishotStartOffsetAngle + multishotOffsetPerI * i);
            SpawnProjectileWithDirection(position, thisDirection, projectileParams);
            if (delayBetweenShots > 0) await UniTask.WaitForSeconds(delayBetweenShots);
        }
    }
}
