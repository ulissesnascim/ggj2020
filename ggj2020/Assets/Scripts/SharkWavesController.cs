using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkWavesController : MonoBehaviour
{
    [SerializeField] private AnimationCurve waterLevelToSpawnTime = null;
    [SerializeField] private float minSpawnTime = 0;
    [SerializeField] private float maxSpawnTime = 0;
    [SerializeField] private SharkBehaviour initialShark = null;

    private WaterLevel waterLevel;
    private SharkBehaviour[] sharks;
    private float timer = 0;
    private float timeToSpawnShark = 0;
    private float sharkTimeCoefficient = 0;

    void Start()
    {
        waterLevel = FindObjectOfType<WaterLevel>();
        
        //essa linha nao funciona direito sem os objetos estarem ativos
        sharks = GetComponentsInChildren<SharkBehaviour>();

        for (int i = 0; i < sharks.Length; i++)
        {
            sharks[i].gameObject.SetActive(false);
        }

        SpawnShark(initialShark); //só pro jogador ver ~o perigo logo no inicio

        sharkTimeCoefficient = (maxSpawnTime - minSpawnTime);
    }

    void Update()
    {
        CalculateSharkTimeToSpawn();

        timer += Time.deltaTime;

        if (timer > timeToSpawnShark)
        {
            if (!AllSharksActive())
            {
                SpawnShark(SelectRandomShark());
            }

            timer = 0;

        }


    }

    private bool AllSharksActive()
    {
        bool allSharksActive = true;

        for (int i = 0; i < sharks.Length; i++)
        {
            if (!sharks[i].gameObject.activeInHierarchy)
            {
                allSharksActive = false;
                break;
            }
        }

        return allSharksActive;
    }

    private void CalculateSharkTimeToSpawn()
    {
        float waterLevelPercentile = 1 - waterLevel.CurrentWaterLevel / waterLevel.MaximumWaterLevel;
        timeToSpawnShark = minSpawnTime + waterLevelToSpawnTime.Evaluate(waterLevelPercentile) * sharkTimeCoefficient;
        
    }

    private SharkBehaviour SelectRandomShark()
    {
        SharkBehaviour shark = sharks[Random.Range(0, sharks.Length)];

        int whileBreaker = 0;
        bool stopLoop = false;

        while (!stopLoop)
        {
            whileBreaker++;

            shark = sharks[Random.Range(0, sharks.Length)];
            
            stopLoop = !shark.gameObject.activeInHierarchy;

            if (whileBreaker > 500)
            {
                Debug.LogWarning("Loop infinito! Rever lógica");
                break;
            }
        }

        return shark;

    }

    private void SpawnShark(SharkBehaviour shark) 
    {
        shark.gameObject.SetActive(true);

    }
}
