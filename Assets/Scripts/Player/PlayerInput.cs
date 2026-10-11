using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;

    PlayerInputState inputState;

    PlayerMovement _playerMovement;

    [Inject]
    public void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        inputState = PlayerInputState.CanControl;
    }

    void Update()
    {
        CheckPlayerInputState();
        var inputInfo = inputState switch
        {
            PlayerInputState.CanControl => InputCanCotrol(),
            PlayerInputState.DoingOther => InputDoingOther(),
            _ => throw new NotImplementedException("This input state is not implemented"),
        };
        _playerMovement.ProvideInput(inputInfo);
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

    enum PlayerInputState
    {
        CanControl,
        DoingOther
    }
}
