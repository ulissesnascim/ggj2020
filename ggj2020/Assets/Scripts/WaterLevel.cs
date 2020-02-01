using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float CurrentWaterLevel;
    public float MaximumWaterHeight;

    private void Update()
    {     
        transform.position = new Vector3(transform.position.x, CurrentWaterLevel, transform.position.z);
    }
}
