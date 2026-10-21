using System;
using Zenject;

public class GameplayState : BaseGameState
{
    [Inject] readonly Info gameplayStateInfo;

    [Inject] readonly InputManager _inputManager;

    protected override string SceneName => gameplayStateInfo.sceneName;

    public override void OnEnter()
    {
        _inputManager.SwitchToGame();
    }

    [Serializable]
    public struct Info
    {
        public string sceneName;
    }
}
