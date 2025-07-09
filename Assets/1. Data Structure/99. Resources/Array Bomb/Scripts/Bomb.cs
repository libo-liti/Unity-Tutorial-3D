using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody _bombRb;

    private float _bombRange = 10f;
    private float _bombTime = 4f;

    public LayerMask _layerMask;
    
    private void Awake()
    {
        _bombRb = GetComponent<Rigidbody>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_bombTime);

        BombForce();
    }

    private void BombForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _bombRange, _layerMask);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            
            rb.AddExplosionForce(1000f, transform.position, _bombRange, 1f);
        }
        Destroy(gameObject);
    }
}
