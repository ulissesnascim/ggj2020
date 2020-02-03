using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerGrab : MonoBehaviour
{
    public enum PlayerType { Player01, Player02 };
    [Header("Current Player")]
    public PlayerType currentPlayer;

    private enum RaycastHitType { Hole, Object, None };

    [Space(5f)]
    [SerializeField] private float timeToDestroyAfterDiscarded = 0;
    [SerializeField] private Transform grabbedItemTransformParent = null;

    [Header("Player Inputs")]
    public KeyCode interactionKey = KeyCode.U;
    public KeyCode discardKey = KeyCode.I;
    public KeyCode bucketKey = KeyCode.O;

    private LayerMask layersToIgnoreWhenRaycasting;

    private GameObject itemReadyToGrab;
    private Hole holeReadyToClose;
    private GrabbableItem grabbedItem;

    private bool isCoveringHole;
    private Vector3 _positionToSpawnRaycast;

    private bool bucketReady;

    public BucketBehaviour bucket;

    public ArmBehaviour arm;

    FirstPersonController firstPersonController;

    public Camera canera;

    public float holeReachDistance;

    // Start is called before the first frame update
    void Start()
    {
        layersToIgnoreWhenRaycasting = LayerMask.GetMask("BoatInvisibleWalls", "Player");
        firstPersonController = GetComponent<FirstPersonController>();
        UnreadyBucket();
    }

    private RaycastHitType CastRayCast()
    {
        Ray ray = canera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);
        LayerMask layerMask = ~layersToIgnoreWhenRaycasting;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            if (raycastHit.collider)
            {
                if (raycastHit.transform.tag == "GrabableObject")
                {
                    itemReadyToGrab = raycastHit.rigidbody.gameObject;
                    return RaycastHitType.Object;
                }

                if (raycastHit.transform.tag == "Hole" && raycastHit.distance <= holeReachDistance)
                {
                    holeReadyToClose = raycastHit.collider.GetComponent<Hole>();
                    return RaycastHitType.Hole;
                }
            }
        }

        return RaycastHitType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if(bucketReady)
            {
                bucket.Interact();
            }
            else
            {
                RaycastHitType hitType = CastRayCast();

                print(hitType);
                if (!grabbedItem && hitType == RaycastHitType.Object)
                {
                    GrabItem(itemReadyToGrab);
                }
                else if (hitType == RaycastHitType.Hole)
                {
                    if (grabbedItem)
                    {
                        if ((int)grabbedItem.itemSize <= (int)holeReadyToClose.holeSize)
                            CloseHoleWithGrabbedItem(holeReadyToClose);
                    }
                    else
                    {
                        StartCoveringHole();
                    }
                }
            }
        }

        if (Input.GetKeyUp(interactionKey))
        {
            if (isCoveringHole)
            {
                UncoverHole();
            }
        }

        if (Input.GetKeyDown(discardKey))
        {
            if (grabbedItem)
            {
                DiscardItem();
            }
        }

        if(Input.GetKeyDown(bucketKey) && !grabbedItem && !isCoveringHole)
        {
            ReadyBucket();
        }
        
        if(Input.GetKeyUp(bucketKey))
        {
            UnreadyBucket();
        }
    }

    private void ReadyBucket()
    {
        bucket.gameObject.SetActive(true);
        bucketReady = true;
    }

    private void UnreadyBucket()
    {
        bucket.DropBucket();
        bucketReady = false;
    }

    private void StartCoveringHole()
    {
        isCoveringHole = true;

        holeReadyToClose.CoverHole();

        firstPersonController.CanMove = false;

        arm.Show();
    }

    private void UncoverHole()
    {
        isCoveringHole = false;
        holeReadyToClose.UncoverHole();
        firstPersonController.CanMove = true;
        arm.Hide();
    }

    private void CloseHoleWithGrabbedItem(Hole hole)
    {
        grabbedItem.LockToHole(hole);

        grabbedItem = null;
    }

    private void GrabItem(GameObject item)
    {
        grabbedItem = itemReadyToGrab.GetComponent<GrabbableItem>();
        grabbedItem.ItemGrabbed();

        grabbedItem.transform.SetParent(grabbedItemTransformParent, true); //tem que dar true senao ele refaz a escala
        grabbedItem.transform.position = grabbedItemTransformParent.position;
        grabbedItem.transform.localPosition = Vector3.zero;
        grabbedItem.transform.localEulerAngles = Vector3.zero;

        itemReadyToGrab = null;

    }

    private void DiscardItem()
    {
        grabbedItem.ItemDiscarded(timeToDestroyAfterDiscarded);

        grabbedItem.transform.SetParent(null);

        grabbedItem = null;
    }
}
