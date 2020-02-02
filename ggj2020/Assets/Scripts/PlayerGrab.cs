using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private Transform grabbedItemTransformParent;
    [SerializeField] private KeyCode grabButtonMouse;
    [SerializeField] private KeyCode discardButtonMouse;
    [SerializeField] private KeyCode grabButtonGamepad;
    [SerializeField] private KeyCode discardButtonGamepad;

    private LayerMask layersToIgnoreWhenRaycasting;

    private GameObject itemReadyToGrab;
    private GrabbableItem grabbedItem;
    private Rigidbody grabbedItemRigidbody;

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

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            if (raycastHit.rigidbody)
            {
                if (raycastHit.rigidbody.gameObject.GetComponent<GrabbableItem>())
                {
                    itemReadyToGrab = raycastHit.rigidbody.gameObject;
                }
                else
                {
                    itemReadyToGrab = null;
                }
            }
        }

        if (Input.GetKeyDown(grabButtonMouse) || Input.GetKeyDown(grabButtonGamepad))
        {
            if (itemReadyToGrab)
            {
                GrabItem(itemReadyToGrab);
                itemReadyToGrab = null;
            }
            else if (grabbedItem)
            {
                //tapa buraco
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

    private void GrabItem(GameObject item)
    {
        grabbedItem = itemReadyToGrab.GetComponent<GrabbableItem>();
        grabbedItemRigidbody = grabbedItem.GetComponent<Rigidbody>();

        itemReadyToGrab.transform.SetParent(grabbedItemTransformParent, true); //tem que dar true senao ele refaz a escala
        itemReadyToGrab.transform.position = grabbedItemTransformParent.position;
        itemReadyToGrab.transform.localPosition = Vector3.zero;
        itemReadyToGrab.transform.localPosition = Vector3.zero;
    }

    private void DiscardItem()
    {
        grabbedItemRigidbody.isKinematic = false;
        grabbedItemRigidbody.useGravity = true;

        grabbedItem.transform.SetParent(null);

        grabbedItem = null;
    }
}
