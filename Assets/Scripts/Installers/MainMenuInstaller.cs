using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MainMenuManager>().AsSingle();
        Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle();
    }
}