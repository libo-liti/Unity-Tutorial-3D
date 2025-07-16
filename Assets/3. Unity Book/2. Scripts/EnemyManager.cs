using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private float _currentTime;
    private float minTime = 1f;
    private float maxTime = 5f;
    public float createTime = 1f;

    public GameObject enemyFactory;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > createTime)
        {
           GameObject enemy = Instantiate(enemyFactory);
           enemy.transform.position = transform.position;

            _currentTime = 0f;
        }
    }
}
