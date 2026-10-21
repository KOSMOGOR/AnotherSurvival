using System;
using Zenject;

public class MainMenuState : BaseGameState
{
    [Inject] readonly Info mainMenuStateInfo;

    [Inject] readonly InputManager _inputManager;

    protected override string SceneName => mainMenuStateInfo.sceneName;

    public override void OnEnter()
    {
        _inputManager.SwitchToUI();
    }

    [Serializable]
    public struct Info
    {
        public string sceneName;
    }
}
