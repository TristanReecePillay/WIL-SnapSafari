using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // Array to hold different animal prefabs
    public int numberOfEachAnimal = 5; // Number of each animal to spawn
    public float spawnRadius = 20f; // Radius within which animals will spawn

   
    // Start is called before the first frame update
    void Start()
    {
        // Check if NavMesh is present in the scene
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.LogError("No NavMesh found in the scene!");
            return;
        }

        // Loop to spawn animals
        for (int i = 0; i < numberOfEachAnimal; i++)
        {
            // Get a random position within the spawn radius
            Vector3 randomSpawnPos = GetRandomPosition();

            // Find the closest point on NavMesh to the random position
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(randomSpawnPos, out navMeshHit, spawnRadius, NavMesh.AllAreas))
            {
                // Instantiate a random animal prefab at the sampled position with no rotation
                GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
                GameObject newAnimal = Instantiate(animalPrefab, navMeshHit.position, Quaternion.identity);
                newAnimal.transform.parent = transform; // Parent the spawned animal

                // Debug to verify spawned positions in the console
                Debug.Log("Spawned at: " + navMeshHit.position);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position for animal " + i);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random position within a circle defined by spawnRadius
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.y = Terrain.activeTerrain.SampleHeight(randomPos) + 1f; // Adjust Y to terrain height

        return randomPos;
    }
    
}
