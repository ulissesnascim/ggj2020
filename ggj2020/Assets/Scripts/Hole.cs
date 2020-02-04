﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public enum HoleSize { Large, Medium, Small }; //NAO MUDAR ORDEM -- INT USADO COMO CONDIÇÃO PARA LOCKTOHOLE

    public HoleSize holeSize;
    public ParticleSystem ParticleSystem;

    [Space(3f)]
    [Header("Vazao de cada buraco")]
    public float SmallFlowRate;
    public float MediumFlowRate;
    public float LargeFlowRate;

    private float _flowRate;
    private float _totalWaterFlow = 0;
    private bool _isOpen = true;
    private float overlapSphereRadius = 1f;
    private ParticleSystem.EmissionModule _emission;

    private void Start()
    {
        _emission = ParticleSystem.emission;
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
                    if (grabbableItem.itemState == GrabbableItem.GrabbableItemState.Discarded)
                    {
                        if (!grabbableItem.negativeItem)
                        {
                            if ((int)grabbableItem.itemSize <= (int)holeSize)
                            {
                                grabbableItem.LockToHole(this);

                            }
                        }
                        else
                        {
                            grabbableItem.TearLargerHole(this);
                        }

                    }
                        
                }

            }
        }
    }

    public void CoverHole()
    {
        _emission.rateOverTime = 0;
        _isOpen = false;
    }

    public void UncoverHole()
    {
        _emission.rateOverTime = 24f;
        _isOpen = true;
    }

    private void Update()
    {
        if (_isOpen)
        {
            WaterLevel.instance.AddWater(_flowRate * Time.deltaTime);
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

    private void OnDestroy()
    {
        HolesBehaviour.instance.HoleDestroyed((int)holeSize);
    }
}
