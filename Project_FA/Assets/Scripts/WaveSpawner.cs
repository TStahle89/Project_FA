using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]

public class Wave
{
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _nextSpawnTime;
    private bool _canSpawn = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentWave = waves[_currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");
        if (totalEnemies.Length == 0 && !_canSpawn && _currentWaveNumber+1 != waves.Length)
        {
            _currentWaveNumber++;
            _canSpawn = true;
        }
    }

    void SpawnWave()
    {
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.typeOfEnemies[Random.Range(0, _currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;
            if (_currentWave.numberOfEnemies == 0)
            {
                _canSpawn = false;
            }
        }
    }
    
}
