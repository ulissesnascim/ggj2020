using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableItem : MonoBehaviour
{
    private bool _gravityOn = false;
    private float _modifier = 1;

    private void FixedUpdate()
    {
        if (_gravityOn)
        {
            transform.Translate(Vector3.down * _modifier / 100);

        }

    }

    //ESSA GRAVIDADE NAO ESTÁ SENDO USADA
    public void ActivateGravity(float modifier)
    {
        _modifier = modifier;
        _gravityOn = true;

    }


    public void DeactivateGravity()
    {
        _gravityOn = false;

    }

}
