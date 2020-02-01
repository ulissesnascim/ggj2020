using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankDestroy : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void DestroyPlank(GameObject plank)
    {
        Destroy(plank);
    }

    private void OnTriggerEnter(Collider other)
    {

        //mudar esse código depois
        if (other.name != "Sea")
            DestroyPlank(other.gameObject);


    }


}

