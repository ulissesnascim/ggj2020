using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float CurrentWaterLevel;
    public float MaximumWaterHeight = 0.75f;

    // Variaveis de controle interno
    private float _maximumWaterLevel = 10f;
    private float _waterGrowthRate;

    private void Start()
    {
        _waterGrowthRate = MaximumWaterHeight / _maximumWaterLevel;
    }

    private void Update()
    {
        if (CurrentWaterLevel < _maximumWaterLevel)
            transform.position = new Vector3(transform.position.x, CurrentWaterLevel * _waterGrowthRate, transform.position.z);
        //else
          //  Debug.Log("Voce perdeu");        
    }
}
