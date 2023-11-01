using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float maxDistance = 2.0f;
    public LayerMask terrainLayer; // Assign the terrain layer in the Inspector

    public WaterSurfaceEffect waterSurfaceEffect;

    private Vector3 targetPosition;

    private void Start()
    {
        GenerateRandomTarget();
    }

    private void Update()
    {
        
        RotateTowardsTarget(targetPosition);
        transform.LookAt((targetPosition)); //Animals look towards target but still walk sideways for some reason

        if (Vector3.Distance(transform.position, targetPosition) < 1.0f)  
        {
            GenerateRandomTarget();
        }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (waterSurfaceEffect != null && waterSurfaceEffect.isInWater)
        {
            // Reduce the animal's movement speed when in the water
            transform.Translate(moveDirection * moveSpeed * waterSurfaceEffect.reducedSpeed * Time.deltaTime);
        }
        else
        {
            // Normal movement speed on land
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        
        //ConstrainToTerrain();
    }

    private void GenerateRandomTarget()
    {
        float randomX = Random.Range(-maxDistance, maxDistance);
        float randomZ = Random.Range(-maxDistance, maxDistance);
        Vector3 randomOffset = new Vector3(randomX, 0, randomZ);

        targetPosition = transform.position + randomOffset;

        RaycastHit hit;
        if (Physics.Raycast(targetPosition + Vector3.up * 10.0f, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            targetPosition = hit.point + Vector3.up * 1.0f; ;
        }
        else
        {
            // Handle the case where no terrain is hit (e.g., if terrainLayer is not set correctly)
            Debug.LogWarning("Target position is not on the terrain.");
        }
    }

    private void ConstrainToTerrain()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 10.0f, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            transform.position = hit.point;
        }

    }

    private void RotateTowardsTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 0.5f); // You can adjust the size and color as needed
    }
}
