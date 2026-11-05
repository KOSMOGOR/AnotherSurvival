using Aarthificial.Reanimation;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Reanimator))]
public class PlayerPresenter : MonoBehaviour
{
    [Inject] readonly PlayerMovement _playerMovement;

    Reanimator reanimator;

    void Awake()
    {
        reanimator = GetComponent<Reanimator>();
    }

    void Update()
    {
        reanimator.Set(Drivers.isMoving, _playerMovement.IsMoving);
        reanimator.Set(Drivers.moveDirClock, MathHelper.DirectionToClock(_playerMovement.Forward, 8));
    }

    static class Drivers
    {
        public const string isMoving = "isMoving";
        public const string moveDirClock = "moveDirClock";
    }
}
