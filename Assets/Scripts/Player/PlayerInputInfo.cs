using System;
using UnityEngine;

[Serializable]
public struct PlayerInputInfo
{
    public Vector2 moveInput;

    public static PlayerInputInfo Zero => new() { moveInput = Vector2.zero };
}