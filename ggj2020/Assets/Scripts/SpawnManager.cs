﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform leftSpawnPoint = null;
    [SerializeField] private Transform rightSpawnPoint = null;
    [SerializeField] private float minTimeBetweenSpawns = 0;
    [SerializeField] private float maxTimeBetweenSpawns = 0;
    [SerializeField] private List<GameObject> planks = new List<GameObject>();

    private float _spawnTimerLeft = 0;
    private float _spawnTimerRight = 0;

    private float _randomSpawnTimeLeft = 0;
    private float _randomSpawnTimeRight = 0;

    private int _priorIndex = 0;

    private void Start()
    {
        SetRandomSpawnTimeLeft();
        SetRandomSpawnTimeRight();

        _priorIndex = planks.Count + 1;
    }

    private void Update()
    {
        _spawnTimerLeft += Time.deltaTime;
        _spawnTimerRight += Time.deltaTime;

        if (_spawnTimerLeft > _randomSpawnTimeLeft)
        {
            SpawnLeft();
        }

        if (_spawnTimerRight > _randomSpawnTimeRight)
        {
            SpawnRight();
        }

    }

    private void SetRandomSpawnTimeLeft()
    {
        _randomSpawnTimeLeft = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void SetRandomSpawnTimeRight()
    {
        _randomSpawnTimeRight = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void SpawnLeft()
    {
        GameObject plank = Instantiate(RandomPlank(), leftSpawnPoint.position, Quaternion.identity);

        _spawnTimerLeft = 0;
        SetRandomSpawnTimeLeft();
    }

    private void SpawnRight()
    {
        GameObject plank = Instantiate(RandomPlank(), rightSpawnPoint.position, Quaternion.identity);

        _spawnTimerRight = 0;
        SetRandomSpawnTimeRight();
    }

    private GameObject RandomPlank()
    {
        int index = Random.Range(0, planks.Count);
        int whileBreaker = 0;

        //not allowing repeated plank sizes
        while (index == _priorIndex)
        {
            index = Random.Range(0, planks.Count);
            whileBreaker++;

            if (whileBreaker > 500)
            {
                Debug.LogWarning("Loop infinito! Rever lógica");
            }
        }

        _priorIndex = index;

        return planks[index];
    }
}
