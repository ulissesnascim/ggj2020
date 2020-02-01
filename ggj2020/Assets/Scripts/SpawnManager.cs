using System.Collections;
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

    private void Start()
    {
        SetRandomSpawnTimeLeft();
        SetRandomSpawnTimeRight();
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
        GameObject plank = Instantiate(RandomGrabbableObject(), leftSpawnPoint.position, Quaternion.identity);

        _spawnTimerLeft = 0;
        SetRandomSpawnTimeLeft();
    }

    private void SpawnRight()
    {
        GameObject plank = Instantiate(RandomGrabbableObject(), rightSpawnPoint.position, Quaternion.identity);

        _spawnTimerRight = 0;
        SetRandomSpawnTimeRight();
    }

    private GameObject RandomGrabbableObject()
    {
        int index = Random.Range(0, planks.Count);
        return planks[index];
    }
}
