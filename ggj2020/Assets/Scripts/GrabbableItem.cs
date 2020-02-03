using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableItem : MonoBehaviour
{
    public GrabbableItemSize itemSize = GrabbableItemSize.Small;
    [HideInInspector] public GrabbableItemState itemState = GrabbableItemState.OnPlank;

    private float timeBeforeDestroy = 5f;
    private float timer = 0;
    private Rigidbody rb;
    private AudioSource audioSource;

    private BoatController boat;

    public enum GrabbableItemSize
    {
        //NAO MUDAR ORDEM -- USADO COMO CONDIÇÃO PARA LOCKTOHOLE
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
        audioSource = GetComponent<AudioSource>();
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
        //ativado pelo Hole quando está perto de objeto descartado ou pelo PlayerGrab normalmente

        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

        timer = 0;

        rb.isKinematic = true;
        transform.rotation = Quaternion.identity;
        transform.position = hole.transform.position;

        transform.SetParent(boat.transform, true);

        hole.CloseHole();
        itemState = GrabbableItemState.ClosingHole;
    }
      /*     
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

    }*/

}
