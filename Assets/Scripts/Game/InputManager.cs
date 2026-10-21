using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] InputActionReference moveAction;

    PlayerInputState inputState = PlayerInputState.CanControl;
    public PlayerInputInfo InputInfo { get; private set; }

    void Update()
    {
        CheckPlayerInputState();
        InputInfo = inputState switch
        {
            PlayerInputState.CanControl => InputCanCotrol(),
            PlayerInputState.DoingOther => InputDoingOther(),
            _ => throw new NotImplementedException("This input state is not implemented"),
        };
    }

    void CheckPlayerInputState()
    {
        if (EventSystem.current.currentSelectedGameObject == null) inputState = PlayerInputState.CanControl;
        else inputState = PlayerInputState.DoingOther;
    }

    PlayerInputInfo InputCanCotrol()
    {
        return new() { moveInput = moveAction.action.ReadValue<Vector2>() };
    }

    PlayerInputInfo InputDoingOther()
    {
        return PlayerInputInfo.Zero;
    }

    public void SwitchToGame() => SetActionScheme("Player");
    public void SwitchToUI() => SetActionScheme("UI");

    void SetActionScheme(string mapName)
    {
        foreach (InputActionMap map in inputActions.actionMaps)
            if (map.name == mapName) map.Enable();
            else map.Disable();
    }

    enum PlayerInputState
    {
        CanControl,
        DoingOther
    }
}

[Serializable]
public struct PlayerInputInfo
{
    public Vector2 moveInput;

    public static PlayerInputInfo Zero => new() { moveInput = Vector2.zero };
}
