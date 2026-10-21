using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    Player _player;
    PlayerMovement _playerMovement;

    [Inject]
    public void Construct(Player player, PlayerMovement playerMovement)
    {
        _player = player;
        _playerMovement = playerMovement;
    }

    public void ProvideInput(PlayerInputInfo inputInfo) => _playerMovement.ProvideInput(inputInfo);

    public Vector2 Position => _player.Position;
}
