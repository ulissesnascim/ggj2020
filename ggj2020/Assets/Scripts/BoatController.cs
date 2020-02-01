using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public List<GameObject> HolesPrefabs;
    public GameObject HolesPlane;
    
    void Start()
    {
        //InstantiateHole();
    }

    private void InstantiateHole() 
    {
        int r = Random.Range(0, HolesPrefabs.Count);

        GameObject hole = Instantiate(HolesPrefabs[r], HolesPlane.transform.position, Quaternion.identity);
    }
}
