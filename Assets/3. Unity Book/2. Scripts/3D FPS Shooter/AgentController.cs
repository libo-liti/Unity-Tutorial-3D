using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class AgentController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    public Transform[] points;
    public int index;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        agent.SetDestination(points[index].position);
        if (agent.remainingDistance <= 1.5f)
        {
            int temp = index;
            index = Random.Range(0, points.Length);

            if (temp == index)
                index = Random.Range(0, points.Length);
        }
    }
}
