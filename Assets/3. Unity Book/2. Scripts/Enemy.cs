using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 dir;
    public float speed = 5f;

    public GameObject explosionFactory;
    private void Start()
    {
        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 3)
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        else
        {
            dir = Vector3.down;
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * dir;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();

        var score = sm.GetScore() + 1;
        sm.SetScore(score);
        
        
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
