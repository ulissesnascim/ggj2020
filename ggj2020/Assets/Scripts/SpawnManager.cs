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
    [SerializeField] private AnimationCurve probabilityWeightForNumberOfHoles = null;
    [SerializeField] private float maxProbabilityWeight = 0;


    private float _spawnTimerLeft = 0;
    private float _spawnTimerRight = 0;

    private float _randomSpawnTimeLeft = 0;
    private float _randomSpawnTimeRight = 0;

    private int _priorIndex = 0;
    public float[] plankSizeProbabilityWeights;
    public float[] plankSizeProbabilities;
    //public Dictionary<Hole.HoleSize, float> holeSizeProbabilities = new Dictionary<Hole.HoleSize, float>();
    //public Dictionary<Hole.HoleSize, int> holeSizeToPlankIndex = new Dictionary<Hole.HoleSize, int>();


    private void Start()
    {
        SetRandomSpawnTimeLeft();
        SetRandomSpawnTimeRight();

        _priorIndex = planks.Count + 1;
        plankSizeProbabilities = new float[HolesBehaviour.instance.Holes.Count];
        plankSizeProbabilityWeights = new float[HolesBehaviour.instance.Holes.Count];


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
        float sumOfWeights = 0;

        for (int i = 0; i < plankSizeProbabilities.Length; i++)
        {
            int count = HolesBehaviour.instance.currentHoleSizeCounts[i];
            float probabilityBase = HolesBehaviour.instance._holePositions.Count;
            float countPercentile = count / probabilityBase;
            
            float newWeight = 1f + probabilityWeightForNumberOfHoles.Evaluate(countPercentile) * maxProbabilityWeight;
            plankSizeProbabilityWeights[i] = newWeight;


            sumOfWeights += newWeight;
        }

        //novo foreach tem que ser feito para considerar a soma final
        for (int i = 0; i < plankSizeProbabilities.Length; i++)
        {
            plankSizeProbabilities[i] = plankSizeProbabilityWeights[i] / sumOfWeights;
        }        

        int whileBreaker = 0;
        int selectedPlankIndex = _priorIndex;

        while (selectedPlankIndex == _priorIndex)
        {
            float random = Random.value;
            float probabilityDensity = 0;
            whileBreaker++;

            for (int i = 0; i < planks.Count; i++)
            {
                float probability = plankSizeProbabilities[i];

                probabilityDensity += probability;

                if (random < probabilityDensity)
                {
                    selectedPlankIndex = i;
                    break;
                }

            }

            if (whileBreaker > 500)
            {
                Debug.LogWarning("Loop infinito! Rever lógica");
                break;
            }
        }
        
        _priorIndex = selectedPlankIndex;

        return planks[selectedPlankIndex];
    }

}
