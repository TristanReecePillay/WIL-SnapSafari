using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    //public float moveSpeed = 2.0f;
    public float maxDistance = 2.0f;
    public LayerMask terrainLayer; // Assign the terrain layer in the Inspector
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    public WaterSurfaceEffect waterSurfaceEffect;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Vector3 newPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        agent.transform.position = newPosition;
        GenerateRandomTarget();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GenerateRandomTarget();
        }

    }

    private void GenerateRandomTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            MoveToTarget(targetPosition);
        }
        else
        {
            Debug.LogWarning("Could not find a valid position on the NavMesh.");
        }


    }

    private void MoveToTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 0.5f); // You can adjust the size and color as needed
    }
}
