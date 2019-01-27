using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public class EntitySpawnEvent: UnityEvent<GameObject> { }

    [Serializable]
    public struct EnemySpawnDef
    {
        public GameObject Prefab;
        public int Rate;
    }

    [Serializable]
    public struct EnemyTypeRateDef
    {
        public EnemyAI.EnemyType EnemyType;
        public int Rate;
    }

    [SerializeField]
    public EntitySpawnEvent OnEntitySpawn;

    [SerializeField]
    private EnemySpawnDef[] _EnemiesToSpawn;
    [SerializeField]
    private float _secondsBetweenWaves = 10;
    [SerializeField]
    private int _waveEnemyCount = 1;

    [SerializeField]
    private EnemyTypeRateDef[] _enemyTypeRates =
        new []
        {
            new EnemyTypeRateDef(){ EnemyType = EnemyAI.EnemyType.Assasin, Rate = 10},
            new EnemyTypeRateDef(){ EnemyType = EnemyAI.EnemyType.Destroyer, Rate = 80},
            new EnemyTypeRateDef(){ EnemyType = EnemyAI.EnemyType.Patrol, Rate = 10},
        };

    private int _totalEnemyTypeRate;
    private int _totalSpawnDefRate;

    private SpawnPoint[] _spawnPoints;
    private EnemyTargetPoint[] _enemyTargetPoints;

    WaveManager()
    {
        OnEntitySpawn = new EntitySpawnEvent();
    }

    void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Assert.IsTrue(_spawnPoints.Length > 0, "SpawnPoints no encontrados");


        _enemyTargetPoints = GetComponentsInChildren<EnemyTargetPoint>();
        Assert.IsTrue(_enemyTargetPoints.Length > 0, "EnemyTargetPoints no encontrados");

        StartCoroutine(_Sequence());

        _totalSpawnDefRate = _EnemiesToSpawn.Sum(spawnDef => spawnDef.Rate);
        _totalEnemyTypeRate = _enemyTypeRates.Sum(rateDef => rateDef.Rate);
    }

    private IEnumerator _Sequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(_secondsBetweenWaves);
            
            for (int i = 0; i < _waveEnemyCount; i++)
            {
                int spawnPointIndx = Random.Range(0, _spawnPoints.Length);
                SpawnPoint spawnPoint = _spawnPoints[spawnPointIndx];

                var enemyType = EnemyAI.EnemyType.Destroyer;
                int enemyTypeRateAcc = Random.Range(0, _totalEnemyTypeRate);

                foreach (EnemyTypeRateDef enemyTypeRate in _enemyTypeRates)
                {
                    if (enemyTypeRateAcc < enemyTypeRate.Rate)
                    {
                        enemyType = enemyTypeRate.EnemyType;
                        break;
                    }
                    else
                    {
                        enemyTypeRateAcc -= enemyTypeRate.Rate;
                    }
                }

                int spawnRateAcc = Random.Range(0, _totalSpawnDefRate);
                foreach (EnemySpawnDef spawnDef in _EnemiesToSpawn)
                {
                    if (spawnRateAcc < spawnDef.Rate)
                    {
                        SpawnEnemyOnPositon(spawnDef.Prefab, spawnPoint.transform, enemyType);
                        break;
                    }
                    spawnRateAcc -= spawnDef.Rate;
                }

            }
        }
    }

    public void SpawnEnemyOnPositon(GameObject prefab, Transform transform, EnemyAI.EnemyType enemyType)
    {
        GameObject enemyGameObject = Instantiate(prefab, transform);

        enemyGameObject.transform.parent = transform;

        Transform targetPoint = _enemyTargetPoints[Random.Range(0, _enemyTargetPoints.Length)].transform;

        enemyGameObject.GetComponent<EnemyAI>().Init(enemyType, targetPoint, new[]{ targetPoint, targetPoint });

        OnEntitySpawn.Invoke(enemyGameObject);

        Debug.LogFormat("Spawn Enemy AI *{0}* Prefab {1}", enemyType, prefab.name);
    }
}
