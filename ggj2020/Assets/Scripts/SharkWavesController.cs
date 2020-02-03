using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkWavesController : MonoBehaviour
{
    public List<GameObject> _waves;
    private WaterLevel _waterLevel;

    void Start()
    {
        _waterLevel = FindObjectOfType<WaterLevel>();

        ActivateWave(0);
    }

    void Update()
    {
        if (_waterLevel.CurrentWaterLevel > _waterLevel.MaximumWaterLevel/3 && _waterLevel.CurrentWaterLevel < (2 * _waterLevel.MaximumWaterLevel) / 3) 
        {          
            if (!_waves[1].activeSelf) 
            { 
                ActivateWave(1);              
            }
        }
        else if (_waterLevel.CurrentWaterLevel >= (2* _waterLevel.MaximumWaterLevel) / 3) 
        {
            if (!_waves[2].activeSelf)
                ActivateWave(2);
        }
    }

    private void ActivateWave(int index) 
    {
        _waves[index].SetActive(true);
    }
}
