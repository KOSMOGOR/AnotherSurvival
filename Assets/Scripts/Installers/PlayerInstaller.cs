using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentOn(transform.parent.gameObject).AsSingle();
        Container.Bind<PlayerMovement>().FromComponentOn(transform.parent.gameObject).AsSingle();
        Container.Bind<PlayerWeapons>().FromInstance(transform.parent.GetComponentInChildren<PlayerWeapons>()).AsSingle();

        Container.Bind<PlayerFacade>().FromComponentOn(transform.parent.gameObject).AsSingle();

        Container.Bind<IAttackPattern>().FromComponentSibling();
        Container.BindFactory<WeaponInfo, IWeapon, WeaponFactory.Factory>().FromFactory<WeaponFactory>();
        Container.BindFactory<ProjectileInfo, ProjectileParams, IProjectile, ProjectileFactory.Factory>().FromFactory<ProjectileFactory>();
    }
}