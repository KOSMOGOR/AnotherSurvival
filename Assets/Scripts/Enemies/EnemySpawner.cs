using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int maxEnemies = 10;
    [SerializeField] Timer enemySpawn = new(0.3f, false);
    [SerializeField] float enemySpawnIndent = 1f;

    readonly List<Enemy> enemies = new();
    bool isRunning = false;

    [Inject] readonly Enemy.Factory _enemyFactory;
    [Inject] readonly GameCamera _gameCamera;

    void Update()
    {
        if (!isRunning) return;
        enemySpawn.Update(Time.deltaTime);
        if (enemies.Count < maxEnemies && enemySpawn.IsCompleteAndReset()) SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Enemy enemy = _enemyFactory.Create();
        enemy.transform.position = GetRandomEnemyPosition();

        enemy.onDeath += OnEnemyDeath;
        enemies.Add(enemy);
    }

    void OnEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    Vector2 GetRandomEnemyPosition()
    {
        Rect cameraRectWorld = _gameCamera.CameraRectWorld;
        float indent = enemySpawnIndent;
        Vector2 indentVector = new(indent, indent);
        Rect[] spawnRects =
        {
            new(cameraRectWorld.position - indentVector, new(cameraRectWorld.width + indent * 2, indent)), // bottom
            new(cameraRectWorld.position.Add(x: -indent), new(indent, cameraRectWorld.height)), // left
            new(cameraRectWorld.position.Add(x: cameraRectWorld.width), new(indent, cameraRectWorld.height)), // right
            new(cameraRectWorld.position.Add(-indent, cameraRectWorld.height), new(cameraRectWorld.width + indent * 2, indent)) // top
        };
        Rect spawnRect = spawnRects.RandomSelectWeight(r => r.Area());
        Vector2 point = spawnRect.GetRandomPointInside();
        return point;
    }

    public void SetRunning(bool isRunning) => this.isRunning = isRunning;
}
