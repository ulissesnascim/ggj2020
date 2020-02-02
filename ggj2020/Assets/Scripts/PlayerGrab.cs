using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        InitPlayerConfiguration();
        layersToIgnoreWhenRaycasting = LayerMask.GetMask("BoatInvisibleWalls", "Player");

    }

    private void InitPlayerConfiguration()
    {
        if (currentPlayer == PlayerType.Player01)
        {
            _positionToSpawnRaycast = new Vector3(Screen.width / 4, Screen.height / 4);
        }
        else
        {
            _positionToSpawnRaycast = new Vector3(Screen.width / 4, Screen.height / 4) * -1;
        }
    }

    private RaycastHitType CastRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(_positionToSpawnRaycast);
        Debug.DrawRay(ray.origin, ray.direction * 100);
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

                if (raycastHit.transform.tag == "Hole")
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
        /*
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        Debug.DrawRay(ray.origin, ray.direction * 100);
        LayerMask layerMask = ~layersToIgnoreWhenRaycasting;

        bool allConditionsMetAux = false;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            if (raycastHit.rigidbody)
            {
                GrabbableItem grabbableItem = raycastHit.rigidbody.gameObject.GetComponent<GrabbableItem>();

                if (grabbableItem && !grabbedItem)
                {
                    if (!(grabbableItem.itemState == GrabbableItem.GrabbableItemState.ClosingHole))
                    {
                        itemReadyToGrab = raycastHit.rigidbody.gameObject;
                        allConditionsMetAux = true;
                    }
                }
            }

            Hole hole = raycastHit.collider.GetComponent<Hole>();

            if (grabbedItem && hole)
            {
                if ((int)grabbedItem.itemSize <= (int)hole.holeSize)
                {
                    holeReadyToClose = hole;

                }
            }
        }

        if (!allConditionsMetAux)
        {
            itemReadyToGrab = null;
        }*/

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
                        if ((int)grabbedItem.itemSize >= (int)holeReadyToClose.holeSize)
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

        if(Input.GetKeyDown(bucketKey))
        {
            ReadyBucket();
        }
        else if(Input.GetKeyUp(bucketKey))
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
        bucket.gameObject.SetActive(false);
        bucketReady = true;
    }

    private void StartCoveringHole()
    {
        isCoveringHole = true;
    }

    private void UncoverHole()
    {

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
