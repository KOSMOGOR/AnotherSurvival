using UnityEngine;
using Zenject;

public class ProjectileReturnToPlayer : BaseProjectile
{
    [Inject] readonly PlayerFacade _playerFacade;

    public override bool TimeToLiveExpired => base.TimeToLiveExpired && Vector2.Distance(_playerFacade.Position, Position) <= 0.1f;

    protected override Vector2 CurrentVelocity => !base.TimeToLiveExpired ? base.CurrentVelocity : (_playerFacade.Position - Position).WithLength(Speed);
}
