using UnityEngine;
using System;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _maxActive = 5;
    [SerializeField] private float _spawnMargin = 2f;
    [SerializeField] private float _spawnMaxExtraDistance = 5f;

    private Player _player;

    private EnemyPool _pool;

    private void Awake()
    {
        _pool = new EnemyPool(_enemyPrefab, 10, _maxActive);

        _player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => _pool.ActiveCount < _maxActive);
            yield return new WaitForSeconds(_spawnInterval);

            if (_pool.ActiveCount >= _maxActive)
                continue; 

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = CameraSpawnUtility.GetPositionOutsideView(
            _gameCamera, _player.transform.position, _spawnMargin, _spawnMaxExtraDistance);

        Enemy enemy = _pool.GetPoolable(spawnPosition, Quaternion.identity);
        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;
        _pool.ReturnToPool(enemy);
    }
}

