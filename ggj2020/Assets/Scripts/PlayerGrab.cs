using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private float timeToDestroyAfterDiscarded = 0;
    [SerializeField] private Transform grabbedItemTransformParent = null;
    [SerializeField] private KeyCode grabButtonMouse = KeyCode.A;
    [SerializeField] private KeyCode discardButtonMouse = KeyCode.A;
    [SerializeField] private KeyCode grabButtonGamepad = KeyCode.A;
    [SerializeField] private KeyCode discardButtonGamepad = KeyCode.A;

    private LayerMask layersToIgnoreWhenRaycasting;

    private GameObject itemReadyToGrab;
    private Hole holeReadyToClose;
    private GrabbableItem grabbedItem;

    // Start is called before the first frame update
    void Start()
    {
        layersToIgnoreWhenRaycasting = LayerMask.GetMask("BoatInvisibleWalls", "Player");

    }

    // Update is called once per frame
    void Update()
    {
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
        }

        if (Input.GetKeyDown(grabButtonMouse) || Input.GetKeyDown(grabButtonGamepad))
        {
            if (itemReadyToGrab)
            {
                GrabItem(itemReadyToGrab);
            }
            else if (grabbedItem)
            {
                CloseHoleWithGrabbedItem(holeReadyToClose);
            }

        }

        if (Input.GetKeyDown(discardButtonMouse) || Input.GetKeyDown(discardButtonGamepad))
        {
            if (grabbedItem)
            {
                DiscardItem();

            }
        }


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
