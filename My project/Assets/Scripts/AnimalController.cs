using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float maxDistance = 2.0f;
    public LayerMask terrainLayer; // Assign the terrain layer in the Inspector

    private Vector3 targetPosition;

    private void Start()
    {
        GenerateRandomTarget();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)  
        {
            GenerateRandomTarget();
        }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        ConstrainToTerrain();
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
            targetPosition = hit.point;
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
}
