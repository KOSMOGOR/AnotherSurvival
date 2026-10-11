using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    EnemySpawner _enemySpawner;

    [Inject]
    public void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }
}
