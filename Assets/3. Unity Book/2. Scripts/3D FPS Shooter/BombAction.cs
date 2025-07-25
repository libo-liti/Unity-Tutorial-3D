using System;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    public int attackPower = 10;
    public float explosionRadius = 5f;
    private void OnCollisionEnter(Collision other)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 7);

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
        }
        
        var eff = Instantiate(bombEffect);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
