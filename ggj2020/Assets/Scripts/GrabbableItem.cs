using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableItem : MonoBehaviour
{
    public GrabbableItemSize itemSize = GrabbableItemSize.Small;
    public bool negativeItem = false;

    [HideInInspector] public GrabbableItemState itemState = GrabbableItemState.OnPlank;
    [SerializeField] private float offsetYFixWhenLockedToHole = 0.5f;

    private float timeBeforeDestroy = 8f;
    private float timer = 0;
    private Rigidbody rb;
    private AudioSource audioSource;
    private float discardForce = 17f;

    private BoatController boat;

    public enum GrabbableItemSize
    {
        //NAO MUDAR ORDEM -- USADO COMO CONDIÇÃO PARA LOCKTOHOLE
        Large, Medium, Small
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
        rb.AddForce(transform.forward * discardForce, ForceMode.Impulse);

        itemState = GrabbableItemState.Discarded;
        timeBeforeDestroy = _timeDiscardedBeforeDestroy;

    }
    
    public void ItemGrabbed()
    {
        timer = 0;
        rb.isKinematic = true;

        itemState = GrabbableItemState.Grabbed;

        transform.localPosition = new Vector3(transform.localPosition.x, 0 + offsetYFixWhenLockedToHole, transform.localPosition.z);


    }

    public void LockToHole(Hole hole)
    {
        //ativado pelo Hole quando está perto de objeto descartado ou pelo PlayerGrab normalmente

        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

        timer = 0;

        rb.isKinematic = true;
        //transform.rotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.position = hole.transform.position;

        transform.SetParent(boat.transform, true);

        transform.localPosition = new Vector3(transform.localPosition.x, 0 + offsetYFixWhenLockedToHole, transform.localPosition.z);

        hole.CloseHole();
        itemState = GrabbableItemState.ClosingHole;
    }

    public void TearLargerHole(Hole hole)
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        Destroy(gameObject);

        if (hole.holeSize != Hole.HoleSize.Large)
        {

            HolesBehaviour.instance.ReplaceHole(hole, (int)hole.holeSize);

        }
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
