using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private ObjectPoolQueue pool;
    private float bulletSpeed = 50f;
    private void Awake()
    {
        pool = FindFirstObjectByType<ObjectPoolQueue>();
    }

    private void Update()
    {
        transform.position += Time.deltaTime * bulletSpeed * Vector3.right;
    }

    private void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }

    private void ReturnPool()
    {
        pool.EnqueueObject(gameObject);
    }
}
