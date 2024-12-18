//using System.Collections.Generic;
//using UnityEngine;

//public class FloorSpawner : MonoBehaviour
//{
//    [Header("Terrain Prefabs")]
//    public GameObject straightRoadPrefab;
//    public GameObject rightTurnPrefab;
//    public GameObject leftTurnPrefab;

//    [Header("Spawn Settings")]
//    public Transform carTransform; // Reference to the car's transform
//    public int maxTilesOnScreen = 10; // Max number of tiles active at once
//    public float spawnDistance = 50f; // Distance ahead of the car to spawn the next tile
//    public float yOffset = -0.1f; // Offset to ensure the tile spawns just below the car to avoid collisions

//    private Vector3 currentSpawnPosition = Vector3.zero; // Position where the next tile spawns
//    private Quaternion currentRotation = Quaternion.identity; // Rotation for spawning the next tile
//    private Queue<GameObject> spawnedTiles = new Queue<GameObject>(); // Active tiles

//    void Start()
//    {
//        // Initial tiles to start the endless runner
//        SpawnInitialTiles();
//    }

//    void Update()
//    {
//        // Keep spawning new tiles based on the car's movement
//        // Check if the car is approaching the spawn position
//        if (Vector3.Distance(carTransform.position, currentSpawnPosition) < spawnDistance)
//        {
//            SpawnNextFloor();
//        }
//    }

//    private void SpawnInitialTiles()
//    {
//        // Spawn a few tiles at the start to get things rolling
//        for (int i = 0; i < 5; i++) // Adjust the number of initial tiles
//        {
//            SpawnNextFloor();
//        }
//    }

//    private void SpawnNextFloor()
//    {
//        // Randomly select a prefab (straight, left-turn, or right-turn)
//        int randomFloor = Random.Range(0, 3);
//        GameObject selectedPrefab = straightRoadPrefab;

//        switch (randomFloor)
//        {
//            case 0: // Straight road
//                selectedPrefab = straightRoadPrefab;
//                break;
//            case 1: // Right turn
//                selectedPrefab = rightTurnPrefab;
//                currentRotation *= Quaternion.Euler(0, 90, 0); // Turn right
//                break;
//            case 2: // Left turn
//                selectedPrefab = leftTurnPrefab;
//                currentRotation *= Quaternion.Euler(0, -90, 0); // Turn left
//                break;
//        }

//        // Spawn the selected tile in front of the car
//        SpawnTileInDirection(selectedPrefab, carTransform.forward);

//        // Remove old tiles if exceeding the limit
//        if (spawnedTiles.Count > maxTilesOnScreen)
//        {
//            GameObject oldTile = spawnedTiles.Dequeue();
//            Destroy(oldTile);
//        }
//    }

//    private void SpawnTileInDirection(GameObject prefab, Vector3 direction)
//    {
//        // Get the size of the prefab to ensure it aligns perfectly with the previous tile
//        Vector3 prefabSize = GetPrefabSize(prefab);

//        // Calculate the spawn position to ensure the tile spawns right next to the car
//        Vector3 spawnPos = carTransform.position + direction * prefabSize.z;

//        // Sample the terrain height at the spawn position to ensure no gaps or overlaps
//        spawnPos.y = SampleTerrainHeight(spawnPos);

//        // Spawn the tile at the adjusted position with the current rotation
//        GameObject newTile = Instantiate(prefab, spawnPos, currentRotation);
//        spawnedTiles.Enqueue(newTile); // Add to the active tiles queue

//        // Update the current spawn position to the new tile's position
//        currentSpawnPosition = spawnPos;
//    }

//    private float SampleTerrainHeight(Vector3 spawnPos)
//    {
//        // Sample the terrain height at the spawn position to avoid gaps
//        Terrain terrain = Terrain.activeTerrain; // Assuming the terrain is in the scene
//        if (terrain != null)
//        {
//            // Get the height at the given position (world space)
//            float terrainHeight = terrain.SampleHeight(spawnPos);
//            return terrainHeight + yOffset; // Adjust with yOffset for proper collision avoidance
//        }
//        return spawnPos.y; // Return the original Y position if no terrain is present
//    }

//    private Vector3 GetPrefabSize(GameObject prefab)
//    {
//        // Determine the size of the prefab (supports both Terrain and Mesh Renderer)
//        Terrain terrain = prefab.GetComponentInChildren<Terrain>();
//        if (terrain != null)
//        {
//            // If it's a Terrain prefab, return the terrain size
//            return terrain.terrainData.size;
//        }

//        // If it's a mesh prefab, return the size based on the mesh renderer
//        Renderer renderer = prefab.GetComponentInChildren<Renderer>();
//        if (renderer != null)
//        {
//            return renderer.bounds.size;
//        }

//        // Log an error if no valid component is found
//        Debug.LogError("Prefab must have a Terrain or Renderer component.");
//        return Vector3.zero;
//    }
//}




using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [Header("Terrain Prefabs")]
    public GameObject straightRoadPrefab;

    [Header("Spawn Settings")]
    public Transform carTransform; // Reference to the car's transform
    public int maxTilesOnScreen = 10; // Max number of tiles active at once

    private Vector3 currentSpawnPosition = Vector3.zero; // Position where the next tile spawns
    private Queue<GameObject> spawnedTiles = new Queue<GameObject>(); // Active tiles
    private float tileLength; // Exact length of a tile (calculated automatically)

    void Start()
    {
        // Calculate the tile length using the prefab's Terrain or fallback size
        tileLength = GetTileLength(straightRoadPrefab);

        // Spawn a few initial tiles
        SpawnInitialTiles();
    }

    void Update()
    {
        // Check if the car is approaching the spawn position
        if (Vector3.Distance(carTransform.position, currentSpawnPosition) < tileLength * 2)
        {
            SpawnNextTile();
        }
    }

    private void SpawnInitialTiles()
    {
        for (int i = 0; i < maxTilesOnScreen; i++)
        {
            SpawnNextTile();
        }
    }

    private void SpawnNextTile()
    {
        // Spawn the tile at the current spawn position
        GameObject newTile = Instantiate(straightRoadPrefab, currentSpawnPosition, Quaternion.identity);
        spawnedTiles.Enqueue(newTile);

        // Update the spawn position to move forward by the exact tile length
        currentSpawnPosition += Vector3.forward * tileLength;

        // Remove old tiles if exceeding the limit
        if (spawnedTiles.Count > maxTilesOnScreen)
        {
            GameObject oldTile = spawnedTiles.Dequeue();
            Destroy(oldTile);
        }
    }

    private float GetTileLength(GameObject prefab)
    {
        // Check if the prefab has a Terrain component
        Terrain terrain = prefab.GetComponent<Terrain>();
        if (terrain != null)
        {
            // Get the length of the terrain along the Z-axis
            return terrain.terrainData.size.z;
        }

        // Fallback for non-terrain prefabs (e.g., meshes)
        Renderer renderer = prefab.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.z;
        }

        // Log an error if no valid size is found
        Debug.LogError("Prefab must have a Terrain or Renderer component to calculate tile length!");
        return 10f; // Default length
    }
}
