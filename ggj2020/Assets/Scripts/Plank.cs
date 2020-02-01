using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public Transform ItemInstanceParent;
    [Space(2.5f)]
    public List<GameObject> Itens;
    
    private float _moveSpeed;
    //private Rigidbody _rb;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        _moveSpeed = Random.Range(minSpeed, maxSpeed);
        SpawnItem();
        
        Destroy(gameObject, 5f);
   }

    private void FixedUpdate()
    {
        transform.Translate(-1 * transform.forward * _moveSpeed);
        //_rb.velocity = -1 * transform.forward * _moveSpeed;
    }

    private void SpawnItem() 
    {
        int randomIndex = Random.Range(0, Itens.Count);

        GameObject item = Instantiate(Itens[randomIndex], ItemInstanceParent.position, Quaternion.identity, ItemInstanceParent); 
    }
}
