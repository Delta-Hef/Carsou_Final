using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Reference to the enemy prefab
    public Transform player;             // Reference to the player
    public float spawnRate = 2f;         // How often enemies spawn
    public Transform[] spawnPoints;      // Points where enemies will spawn

    private void Start()
    {
        // Check if spawnPoints is not empty
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned! Please assign spawn points in the Inspector.");
            return; // Exit early if no spawn points are assigned
        }

        // Start spawning enemies at regular intervals
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        // Check if the spawnPoints array has any elements before accessing it
        if (spawnPoints.Length > 0)
        {
            // Select a random spawn point from the spawnPoints array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate a new enemy at the spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get the EnemyAI script attached to the new enemy
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

            // Assign the player reference to the new enemy's AI
            if (enemyAI != null && player != null)
            {
                enemyAI.player = player;
            }
        }
        else
        {
            Debug.LogError("Spawn points array is empty!");
        }
    }
}


