using UnityEngine;
using Zenject;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [field: SerializeField] public WeaponConfig WeaponConfig { get; set; }
    [field: SerializeField, Inject] protected IAttackPattern _attackPattern;

    public int Level { get; private set; } = 1;
    Timer reloadTimer;

    [Inject] protected readonly PlayerFacade _playerFacade;

    protected virtual void Start()
    {
        reloadTimer = new(WeaponConfig.ReloadBase);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        reloadTimer.Update(deltaTime);
        if (reloadTimer.IsComplete && CanAttack())
        {
            reloadTimer.Reset();
            Attack();
        }
    }

    protected virtual bool CanAttack() => true;

    protected abstract void Attack();

    public Vector2 PosToDir(Vector2 pos) => pos - _playerFacade.Position;

    protected virtual void OnTriggerEnter2D(Collider2D collider) {}
    protected virtual void OnTriggerExit2D(Collider2D collider) {}
}
