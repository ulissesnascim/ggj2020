﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public enum HoleSize { Small, Medium, Large }; //NAO MUDAR ORDEM -- INT USADO COMO CONDIÇÃO PARA LOCKTOHOLE

    public HoleSize holeSize;

    [Space(3f)]
    [Header("Vazao de cada buraco")]
    public float SmallFlowRate;
    public float MediumFlowRate;
    public float LargeFlowRate;

    private float _flowRate;
    private float _totalWaterFlow = 0;
    private bool _isOpen = true;
    private WaterLevel _waterLevel;
    private float overlapSphereRadius = 1f;

    private void Start()
    {
        _waterLevel = FindObjectOfType<WaterLevel>();

        SetHoleFlowRate();
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rbAttached = colliders[i].attachedRigidbody;
            
            if (rbAttached)
            {
                GrabbableItem grabbableItem = rbAttached.GetComponent<GrabbableItem>();

                if(grabbableItem)
                {
                    if (grabbableItem.itemState == GrabbableItem.GrabbableItemState.Discarded &&
                        (int) grabbableItem.itemSize <= (int) holeSize)
                    {
                        grabbableItem.LockToHole(this);

                    }

                }

            }


        }
    }

    private void Update()
    {
        if (_isOpen)
        {
            _totalWaterFlow += _flowRate * Time.deltaTime;            
            _waterLevel.CurrentWaterLevel += _totalWaterFlow * Time.deltaTime;
        }
    }

    private void SetHoleFlowRate()
    {
        switch (holeSize)
        {
            case HoleSize.Small:
                _flowRate = SmallFlowRate;
                break;
            case HoleSize.Medium:
                _flowRate = MediumFlowRate;
                break;
            case HoleSize.Large:
                _flowRate = LargeFlowRate;
                break;
            default:
                break;
        }
    }

    public void CloseHole()
    {
        _isOpen = false;
        Destroy(gameObject);

        //Destroy(gameObject, 2.5f);
    }
}
