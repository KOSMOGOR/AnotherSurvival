using Cysharp.Threading.Tasks;

public interface IGameStateMachine
{
    IGameState Current { get; }
    UniTaskVoid Enter<TState>() where TState : class, IGameState;
}

public interface IGameState
{
    UniTask EnterAsync();

    void OnEnter();
    void OnExit();
    void OnUpdate();
}
