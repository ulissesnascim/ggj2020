using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketBehaviour : MonoBehaviour
{
    Animator anim;

    bool isEmpty;

    public float amout;

    public Transform buckets;
    public Transform bucket1;
    public Transform bucket2;

    private void OnEnable()
    {
        buckets.localPosition = Vector3.zero;
        bucket1.localPosition = Vector3.zero;
        bucket2.localPosition = Vector3.zero;
        isEmpty = true;
        anim.Play("BucketEmpty");
    }

    private void OnDisable()
    {
        if(!isEmpty)
        {
            WaterLevel.instance.AddWater(amout);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FillBucket()
    {
        isEmpty = false;
        WaterLevel.instance.RemoveWater(amout);
    }

    public void ThrowWater()
    {
        isEmpty = true;
    }

    public void Interact()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("FillBucket") || anim.GetCurrentAnimatorStateInfo(0).IsName("ThrowAway"))
        {
            return;
        }

        if(isEmpty)
        {
            anim.SetTrigger("Fill");
        }
        else
        {
            anim.SetTrigger("Throw");
        }
    }
}