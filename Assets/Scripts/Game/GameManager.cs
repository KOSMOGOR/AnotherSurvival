using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] readonly InputManager _inputManager;
    [Inject] readonly PlayerFacade _playerFacade;
    EnemySpawner _enemySpawner;

    [Inject]
    public void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
        _enemySpawner.SetRunning(true);
    }

    void Update()
    {
        _playerFacade.ProvideInput(_inputManager.InputInfo);
    }
}
