using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Camera _camera;
    public NavMeshSurface surface;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        surface.transform.position = agent.transform.position;
        surface.BuildNavMesh();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
                transform.SetParent(hit.transform);
            }
        }

        if (Vector3.Distance(transform.position, surface.transform.position) > 20f)
        {
            surface.transform.position = agent.transform.position;
            surface.BuildNavMesh();
        }
    }
}
