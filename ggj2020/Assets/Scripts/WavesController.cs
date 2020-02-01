using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public GameObject wave;

    public int initialPosX;
    public float initialPosY;
    public int initialPosZ;

    public int posModifier;

    public int wavesZ;

    void Awake()
    {
        InstantiateWaves();
    }

    private void InstantiateWaves()
    {
        GameObject instantiatedWave;

        for (int i = 0; i < wavesZ; i++)
        {
            instantiatedWave = Instantiate(wave, new Vector3(initialPosX, initialPosY, initialPosZ - i * posModifier), Quaternion.identity);
            StartCoroutine(instantiatedWave.GetComponent<WaveBehaviour>().WaitAndStartAnim(i
                * 0.5f));
        }
    }
}
