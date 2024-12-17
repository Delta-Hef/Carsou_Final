using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform spawnPoint;     // The central spawner point
    public float spawnRadius = 5f;   // The radius around the spawn point

    public int spawnCount = 1;       // How many prefabs to spawn at once

    void Start()
    {
        // Optional: Start spawning automatically when the scene begins
        SpawnPrefabs();
    }

    public void SpawnPrefabs()
    {
        if (prefabToSpawn == null || spawnPoint == null)
        {
            Debug.LogError("Prefab or Spawn Point not assigned!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            // Generate a random position within the radius
            Vector3 randomPosition = GetRandomPositionWithinRadius();

            // Spawn the prefab at the random position
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        // Generate random offsets within a circle/sphere
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;

        // Calculate the world position based on the spawn point
        Vector3 spawnPosition = new Vector3(
            spawnPoint.position.x + randomCircle.x,
            spawnPoint.position.y,
            spawnPoint.position.z + randomCircle.y
        );

        return spawnPosition;
    }

    // Optional: Call this method from a Button or other trigger
    public void SpawnOnce()
    {
        SpawnPrefabs();
    }
}
