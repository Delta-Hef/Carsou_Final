using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PickUp;                
    public int numberOfPickups = 10;         
    public Vector3 spawnAreaSize = new Vector3(10, 1, 10); 

    private void Start()
    {
        SpawnPickups();
    }

    private void SpawnPickups()
    {
        for (int i = 0; i < numberOfPickups; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnAreaSize.y,  // Fixed height
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            ) + transform.position;

            // Spawn the pickup at the random position
            Instantiate(PickUp, randomPosition, Quaternion.identity);
        }
    }
}



