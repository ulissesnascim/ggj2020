using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform leftSpawnPoint = null;
    [SerializeField] private Transform rightSpawnPoint = null;
    [SerializeField] private float minTimeBetweenSpawns = 0;
    [SerializeField] private float maxTimeBetweenSpawns = 0;
    [SerializeField] private List<GameObject> grabbableObjects = new List<GameObject>();

    private float spawnTimerLeft = 0;
    private float spawnTimerRight = 0;

    private float randomSpawnTimeLeft = 0;
    private float randomSpawnTimeRight = 0;

    private void Start()
    {
        SetRandomSpawnTimeLeft();
        SetRandomSpawnTimeRight();
    }

    private void Update()
    {
        spawnTimerLeft += Time.deltaTime;
        spawnTimerRight += Time.deltaTime;

        if (spawnTimerLeft > randomSpawnTimeLeft)
        {
            SpawnLeft();
        }

        if (spawnTimerRight > randomSpawnTimeRight)
        {
            SpawnRight();
        }

    }

    private void SetRandomSpawnTimeLeft()
    {
        randomSpawnTimeLeft = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void SetRandomSpawnTimeRight()
    {
        randomSpawnTimeRight = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    private void SpawnLeft()
    {
        GameObject plank = Instantiate(RandomGrabbableObject(), leftSpawnPoint.position, Quaternion.identity);

        spawnTimerLeft = 0;
        SetRandomSpawnTimeLeft();
    }

    private void SpawnRight()
    {
        GameObject plank = Instantiate(RandomGrabbableObject(), rightSpawnPoint.position, Quaternion.identity);

        spawnTimerRight = 0;
        SetRandomSpawnTimeRight();       
    }

    private GameObject RandomGrabbableObject()
    {
        int index = Random.Range(0, grabbableObjects.Count - 1);
        return grabbableObjects[index];
    }
}
