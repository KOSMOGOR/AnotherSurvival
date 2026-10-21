using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(gameObject.GetComponentInChildren<InputManager>()).AsSingle();
        Container.Bind<ISceneSwitchManager>().To<SceneSwitchManager>().AsSingle();

        BindGameStateMachine();
    }

    void BindGameStateMachine()
    {
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

        Container.BindInstance<MainMenuState.Info>(new() { sceneName = "MainMenu" });
        Container.Bind<MainMenuState>().AsSingle();
        Container.BindInstance<GameplayState.Info>(new() { sceneName = "Gameplay" });
        Container.Bind<GameplayState>().AsSingle();
    }
}