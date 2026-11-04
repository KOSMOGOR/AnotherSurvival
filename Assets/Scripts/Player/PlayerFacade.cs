using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    Player _player;
    PlayerMovement _playerMovement;
    PlayerWeapons _playerWeapons;

    [Inject]
    public void Construct(Player player, PlayerMovement playerMovement, PlayerWeapons playerWeapons)
    {
        _player = player;
        _playerMovement = playerMovement;
        _playerWeapons = playerWeapons;
    }

    public void ProvideInput(PlayerInputInfo inputInfo) => _playerMovement.ProvideInput(inputInfo);

    public Vector2 Position => _player.Position;
    public Vector2 Forward => Vector2.down; // TODO: implement forward

    public void AddOrUpgradeWeapon(WeaponInfo weaponInfo)
    {
        if (!_playerWeapons.HasWeapon(weaponInfo)) _playerWeapons.AddWeapon(weaponInfo);
        else _playerWeapons.UpgradeWeapon(weaponInfo);
    }
}
