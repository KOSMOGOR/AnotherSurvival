using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] Enemy.EnemySettings enemySettings;
    [SerializeField] Enemy enemyPrefab;

    public override void InstallBindings()
    {
        InstallGame();
        InstallEnemies();
    }

    void InstallGame()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameCamera>().FromComponentInHierarchy().AsSingle();
    }

    void InstallEnemies()
    {
        Container.BindInstance(enemySettings).AsSingle();
        Container.Bind<EnemyGoodZone>().AsSingle();
    
        Container.Bind<EnemyTargetGetter>().AsSingle();
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(enemyPrefab);
    }
}