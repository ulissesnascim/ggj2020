using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public enum HoleSize { Small, Medium, Large };
    public HoleSize holeSize;

    [Space(3f)]
    [Header("Vazao de cada buraco")]
    public float SmallFlowRate;
    public float MediumFlowRate;
    public float LargeFlowRate;

    private float _flowRate;
    private float _totalWaterFlow = 0;
    private bool _isOpen = true;
    private WaterLevel _waterLevel;

    private void Awake()
    {
        _waterLevel = FindObjectOfType<WaterLevel>();
        SetHoleFlowRate();
    }

    private void Update()
    {
        if (_isOpen)
        {
            _totalWaterFlow += _flowRate * Time.deltaTime;            
            _waterLevel.CurrentWaterLevel += _totalWaterFlow * Time.deltaTime;
        }
    }

    private void SetHoleFlowRate()
    {
        switch (holeSize)
        {
            case HoleSize.Small:
                _flowRate = SmallFlowRate;
                break;
            case HoleSize.Medium:
                _flowRate = MediumFlowRate;
                break;
            case HoleSize.Large:
                _flowRate = LargeFlowRate;
                break;
            default:
                break;
        }
    }

    public void CloseHole()
    {
        _isOpen = false;
        Destroy(gameObject, 2.5f);
    }
}
