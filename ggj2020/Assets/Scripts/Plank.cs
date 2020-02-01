using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    private float moveSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Randomiza uma velocidade para a plank entre 3 e 5 (inteiros)
        moveSpeed = Random.Range(3, 6);
        Destroy(gameObject, 5f);
   }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * moveSpeed;
    }

}
