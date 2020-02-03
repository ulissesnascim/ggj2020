using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    public float velocity;
    public float TimeToChangeDirection = 10f;

    private int direction = 1;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeDirection", 0f, TimeToChangeDirection);
        rb = GetComponent<Rigidbody>();
    }

    private void ChangeDirection()
    {
        direction = direction * -1;
        rb.velocity = transform.forward * velocity * direction;
    }
}
