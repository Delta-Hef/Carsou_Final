using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject straightRoadPrefab;
    public GameObject rightTurnPrefab;
    public GameObject leftTurnPrefab;

    private Vector3 currentSpawnPosition = Vector3.zero;
    private Quaternion currentRotation = Quaternion.identity;

    void Start()
    {
        // Spawn the initial straight road
        SpawnFloor(straightRoadPrefab);
    }

    public void SpawnNextFloor()
    {
        int randomFloor = Random.Range(0, 3); // Randomly select 0, 1, or 2
        GameObject selectedPrefab = null;

        switch (randomFloor)
        {
            case 0: // Straight road
                selectedPrefab = straightRoadPrefab;
                break;

            case 1: // Right-turn road
                selectedPrefab = rightTurnPrefab;
                currentRotation *= Quaternion.Euler(0, 90, 0); // Turn 90 degrees to the right
                break;

            case 2: // Left-turn road
                selectedPrefab = leftTurnPrefab;
                currentRotation *= Quaternion.Euler(0, -90, 0); // Turn 90 degrees to the left
                break;
        }

        SpawnFloor(selectedPrefab);
    }

    private void SpawnFloor(GameObject prefab)
    {
        if (prefab != null)
        {
            GameObject newFloor = Instantiate(prefab, currentSpawnPosition, currentRotation);

            // Calculate the next spawn position
            Vector3 offset = prefab.GetComponent<Renderer>().bounds.size;
            currentSpawnPosition += currentRotation * new Vector3(0, 0, offset.z);
        }
    }
}
