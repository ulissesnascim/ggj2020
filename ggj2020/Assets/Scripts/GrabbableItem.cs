using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableItem : MonoBehaviour
{
    private float timeDiscardedBeforeDestroy = 5f;
    private float timer = 0;
    private GrabbableItemState itemState = GrabbableItemState.OnPlank;
    private Rigidbody rb;

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
    }

    private void Update()
    {
        if (itemState == GrabbableItemState.Discarded)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);

            if (timer > timeDiscardedBeforeDestroy)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            timer = 0;
        }

    }
    
    public void ItemDiscarded(float _timeDiscardedBeforeDestroy)
    {
        rb.isKinematic = false;
        rb.useGravity = true;

        itemState = GrabbableItemState.Discarded;
        timeDiscardedBeforeDestroy = _timeDiscardedBeforeDestroy;

    }
    
    public void ItemGrabbed()
    {
        rb.isKinematic = true;

        itemState = GrabbableItemState.Grabbed;

    }

}
