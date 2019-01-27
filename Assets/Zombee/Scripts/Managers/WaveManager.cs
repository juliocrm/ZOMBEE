﻿using System;
using System.Collections;
using System.Collections.Generic;
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
        public float Rate;
    }

    [SerializeField]
    public EntitySpawnEvent OnEntitySpawn;
    [SerializeField]
    private EnemySpawnDef[] _EnemiesToSpawn;
    [SerializeField]
    private float _secondsBetweenWaves = 10;
    [SerializeField]
    private int _waveEnemyCount = 1;

    private SpawnPoint[] _spawnPoints;

    void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Assert.IsTrue(_spawnPoints.Length > 0, "SpawnPoints no encontrados");

        StartCoroutine(_Sequence());

        OnEntitySpawn = new EntitySpawnEvent();
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
                SpawnEnemyOnPositon(_EnemiesToSpawn[Random.Range(0, _EnemiesToSpawn.Length)].Prefab, spawnPoint.transform, Random.Range(0, 2));
            }
        }
    }

    public void SpawnEnemyOnPositon(GameObject prefab, Transform transform, int enemyType)
    {
        GameObject enemyGameObject = Instantiate(prefab, transform);

        //enemyGameObject.transform.position = transform.position;
        //enemyGameObject.transform.rotation = transform.rotation;

        // TODO: Activar al enemigo

        ////////////////

        OnEntitySpawn.Invoke(enemyGameObject);
    }
}