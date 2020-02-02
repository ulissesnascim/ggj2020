using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [HideInInspector] public float CurrentWaterLevel;
    public float MaximumWaterHeight = 0.75f;
    public float MaximumWaterLevel = 10f;

    // Variaveis de controle interno
    private float _waterGrowthRate;

    private void Start()
    {
        CurrentWaterLevel = 0;
        _waterGrowthRate = MaximumWaterHeight / MaximumWaterLevel;
    }

    private void Update()
    {
        if (CurrentWaterLevel < MaximumWaterLevel)
            transform.position = new Vector3(transform.position.x, CurrentWaterLevel * _waterGrowthRate, transform.position.z);
        else
            Debug.Log("Voce perdeu");        
    }
}
