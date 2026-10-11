using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerMovement>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayerInput>().FromComponentInChildren().AsSingle();
    }
}