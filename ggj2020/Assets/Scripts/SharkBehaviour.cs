using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SharkBehaviour : MonoBehaviour
{
    public int velocity;
    public Transform VFX;

    private int direction = 1;
    private float _initialVerticalPos;
    private Rigidbody rb;
    private Sequence _sequence;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeDirection", 0f, 2.5f);
        _initialVerticalPos = VFX.position.y;
        rb = GetComponent<Rigidbody>();
    }

    private void ChangeDirection() 
    {
        direction = direction * -1;
        rb.velocity = transform.forward * velocity * direction;
    }
}
