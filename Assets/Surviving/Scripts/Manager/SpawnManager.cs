using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private int _maxEnemiesInScene;

    [SerializeField] private bool _spawnBoss;
    [SerializeField] private float _timeForSpawningBoss;

    [SerializeField] private float _spawnTime;

    private List<GameObject> _enemiesSpawned;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _enemiesSpawned = new List<GameObject>();

        StartCoroutine(SpawnEnemyTimer());
    }
    private void Update()
    {
        if ( _timeForSpawningBoss == GameManager.instance._timePassed )
        { 
            if (_timeForSpawningBoss == 0 ) return;

            _spawnBoss = true;
        }
    }

    private IEnumerator SpawnEnemyTimer()
    {
        while (true)
        {
            yield return new WaitWhile(() => _enemiesSpawned.Count >= _maxEnemiesInScene);

            SpawnEnemy();

            yield return new WaitForSeconds(_spawnTime);
        }
    }


    private void SpawnEnemy()
    {
        var newPosition = new Vector3(
            _player.transform.position.x + Random.Range(-15f, 16f),
            0,
            _player.transform.position.z + Random.Range(-15f, 16f)
            );


        if (_spawnBoss == true)
        {
            _spawnBoss = false;

            var goBoss = Instantiate(_enemies[1], newPosition, Quaternion.Euler(new Vector3(-20.47f, 180, 0)));
            _enemiesSpawned.Add(goBoss);
        }

        var go = Instantiate(_enemies[0], newPosition, Quaternion.Euler(new Vector3(-20.47f, 180, 0)));
        _enemiesSpawned.Add(go);
       
    }

    public void RemoveEnemy(GameObject go)
    {
        _enemiesSpawned.Remove(go);
    }

}
