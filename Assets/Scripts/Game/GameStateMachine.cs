using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameStateMachine : IGameStateMachine, IInitializable, ITickable
{
    readonly Dictionary<Type, IGameState> _states;

    public IGameState Current { get; private set; }

    bool changingState = false;

    public GameStateMachine(
        // BootState boot,
        MainMenuState mainMenu,
        // LoadingState loading,
        GameplayState gameplay)
    {
        _states = new()
        {
            // [typeof(BootState)] = boot,
            [typeof(MainMenuState)] = mainMenu,
            // [typeof(LoadingState)] = loading,
            [typeof(GameplayState)] = gameplay,
        };
    }

    public void Initialize() => Enter<MainMenuState>().Forget();

    public void Tick()
    {
        Current?.OnUpdate();
    }

    public async UniTaskVoid Enter<TState>() where TState : class, IGameState
    {
        if (changingState) return;
        changingState = true;
        await EnterInternal<TState>();
        changingState = false;
    }

    async UniTask EnterInternal<TState>() where TState : class, IGameState
    {
        Current?.OnExit();
        Current = _states[typeof(TState)];
        await Current.EnterAsync();
        Current.OnEnter();
    }
}