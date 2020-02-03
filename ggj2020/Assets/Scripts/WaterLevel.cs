using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float CurrentWaterLevel = 0f;
    public float MaximumWaterHeight = 0.75f;
    public float MaximumWaterLevel;

    // Variaveis de controle interno
    private float _waterGrowthRate;
    private bool _hasWaterReachedTheMaximunLevel = false;
    private float _initialVerticalPos;
    //private float debugLevel = 0;

    public static WaterLevel instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        _initialVerticalPos = transform.position.y;
        _waterGrowthRate = MaximumWaterHeight / MaximumWaterLevel;

    }

    public void AddWater(float amount)
    {
        CurrentWaterLevel += amount;

        /*if (amount < 0)
        {
            Debug.LogWarning("ow");
        }*/
    }

    public void RemoveWater(float amount)
    {
        CurrentWaterLevel -= amount;
        
        /*if (amount < 0)
        {
            Debug.LogWarning("ow");
        }*/
    }

    private void Update()
    {
        /*if (debugLevel > CurrentWaterLevel)
            Debug.LogWarning(debugLevel - CurrentWaterLevel);*/
        
        if (CurrentWaterLevel < MaximumWaterLevel) 
        { 
            transform.position = new Vector3(transform.position.x, _initialVerticalPos + (CurrentWaterLevel * _waterGrowthRate), transform.position.z);       
        }
        else 
        { 
            if (!_hasWaterReachedTheMaximunLevel) 
            {               
                _hasWaterReachedTheMaximunLevel = true;
            }
        }
        
        //debugLevel = CurrentWaterLevel;

    }
}
