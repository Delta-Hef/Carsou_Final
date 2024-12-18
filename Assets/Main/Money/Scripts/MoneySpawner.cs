using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform spawnPoint;     // The central spawner point
    public float spawnRadius = 5f;   // The radius around the spawn point

    public int spawnCount = 1;       // How many prefabs to spawn at once
    public bool randomRotation = true; // Whether to apply random rotation to the spawned prefabs

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

            // Determine rotation
            Quaternion rotation = randomRotation ? GetRandomRotation() : Quaternion.identity;

            // Spawn the prefab at the random position with the selected rotation
            Instantiate(prefabToSpawn, randomPosition, rotation);
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

    private Quaternion GetRandomRotation()
    {
        // Randomly generate a rotation around the Y-axis (you can adjust this to other axes if needed)
        float randomAngle = Random.Range(0f, 360f);  // Random rotation between 0 and 360 degrees
        return Quaternion.Euler(0f, randomAngle, 0f); // Apply only Y-axis rotation
    }

    // Optional: Call this method from a Button or other trigger
    public void SpawnOnce()
    {
        SpawnPrefabs();
    }
}
