﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableItem : MonoBehaviour
{
    [SerializeField] private GrabbableItemSize itemSize = GrabbableItemSize.Small;
    [HideInInspector] public GrabbableItemState itemState = GrabbableItemState.OnPlank;

    private float timeBeforeDestroy = 5f;
    private float timer = 0;
    private Rigidbody rb;
    private BoatController boat;

    public enum GrabbableItemSize
    {
        Small, Medium, Large
    }

    public enum GrabbableItemState
    {
        OnPlank,
        Grabbed,
        Discarded,
        ClosingHole
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boat = FindObjectOfType<BoatController>();

    }

    private void Update()
    {
        if (itemState == GrabbableItemState.Discarded || itemState == GrabbableItemState.ClosingHole)
        {
            timer += Time.deltaTime;

            if (timer > timeBeforeDestroy)
            {
                Destroy(gameObject);
            }

        }

    }
    
    public void ItemDiscarded(float _timeDiscardedBeforeDestroy)
    {
        rb.isKinematic = false;
        rb.useGravity = true;

        itemState = GrabbableItemState.Discarded;
        timeBeforeDestroy = _timeDiscardedBeforeDestroy;

    }
    
    public void ItemGrabbed()
    {
        timer = 0;
        rb.isKinematic = true;

        itemState = GrabbableItemState.Grabbed;

    }

    public void LockToHole(Hole hole)
    {
        timer = 0;

        rb.isKinematic = true;
        transform.rotation = Quaternion.identity;
        transform.position = hole.transform.position;

        transform.SetParent(boat.transform, true);

        hole.CloseHole();
        itemState = GrabbableItemState.ClosingHole;
    }
       
    private void OnTriggerEnter(Collider other)
    {
        if (itemState == GrabbableItemState.Discarded)
        {
            Hole hole = other.GetComponent<Hole>();

            if (hole)
            {
                LockToHole(hole);
            }

        }

    }

}
