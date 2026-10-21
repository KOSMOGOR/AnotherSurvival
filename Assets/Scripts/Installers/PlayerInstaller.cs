using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInParents().AsSingle();
        Container.Bind<PlayerMovement>().FromComponentInParents().AsSingle();

        Container.Bind<PlayerFacade>().AsSingle();
    }
}