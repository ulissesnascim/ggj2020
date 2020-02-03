﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SharkBehaviour : MonoBehaviour
{
    public float velocity;
    public Transform VFX;

    private int direction = 1;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeDirection", 0f, 6f);      
        rb = GetComponent<Rigidbody>();
    }

    private void ChangeDirection() 
    {
        direction = direction * -1;
        rb.velocity = transform.forward * velocity * direction;
    }
}
