using System;

public class MainMenuManager : IDisposable
{
    readonly GameStateMachine _gameStateMachine;
    readonly MainMenuView _mainMenuView;

    public MainMenuManager(GameStateMachine gameStateMachine, MainMenuView mainMenuView)
    {
        _gameStateMachine = gameStateMachine;
        _mainMenuView = mainMenuView;

        _mainMenuView.PlayButton.onClick.AddListener(OnPlayButton);
    }

    void OnPlayButton()
    {
        _gameStateMachine.Enter<GameplayState>().Forget();
    }

    public void Dispose()
    {
        _mainMenuView.PlayButton.onClick.RemoveListener(OnPlayButton);
    }
}
