using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SharkBehaviour : MonoBehaviour
{
    public float velocity;
    public Transform VFX;

    private Vector3 startingPosition;
    private int rot = -1;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;    
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity * -1;
    }

    private void ChangeDirection() 
    {
        rot = rot * -1;
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(0f, 180f * rot, 0f);
        rb.velocity = transform.forward * velocity * -1;
    }

    private void FinishAnimation()
    {
        //called by animation event

        transform.position = startingPosition;
        ChangeDirection();
        gameObject.SetActive(false);


    }
}
