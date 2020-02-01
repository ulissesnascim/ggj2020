using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    public Transform ItemInstanceParent;
    [Space(2.5f)]
    public List<GameObject> Itens;
    
    private float _moveSpeed;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeed = Random.Range(3, 6);
        SpawnItem();
        
        Destroy(transform.parent.gameObject, 5f);
   }

    private void FixedUpdate()
    {
        _rb.velocity = -1 * transform.forward * _moveSpeed;
    }

    private void SpawnItem() 
    {
        int randomIndex = Random.Range(0, Itens.Count);

        GameObject item = Instantiate(Itens[randomIndex], ItemInstanceParent.position, Quaternion.identity, ItemInstanceParent); 
    }
}
