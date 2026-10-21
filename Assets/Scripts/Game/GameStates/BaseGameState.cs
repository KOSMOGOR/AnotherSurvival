using Cysharp.Threading.Tasks;
using Zenject;

public abstract class BaseGameState : IGameState
{
    [Inject] readonly ISceneSwitchManager _sceneSwitchManager;

    protected virtual string SceneName => string.Empty;
    protected bool entering = false;

    public async UniTask EnterAsync()
    {
        if (entering) return;
        entering = true;
        await EnterAsyncInternal();
        entering = false;
    }

    protected virtual async UniTask EnterAsyncInternal()
    {
        await _sceneSwitchManager.SwitchSceneAsync(SceneName);
    }

    public virtual void OnEnter() {}

    public virtual void OnExit() {}

    public virtual void OnUpdate() {}
}
