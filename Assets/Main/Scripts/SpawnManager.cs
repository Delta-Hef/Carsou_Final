using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;   // The enemy prefab to spawn
    public Transform spawnPoint;     // The base point where enemies will spawn
    public float spawnInterval = 5f; // Time interval between enemy spawns
    public int maxEnemies = 2;       // Maximum number of enemies to have on the scene
    public float spawnRange = 10f;   // How far the enemies can spawn from the spawn point

    private void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Count the number of enemies in the scene
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            // If there are fewer than maxEnemies, spawn a new enemy
            if (enemyCount < maxEnemies)
            {
                SpawnEnemy();
            }

            // Wait for a specified amount of time before checking again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Generate a random position around the spawn point within a defined range
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x + randomX, spawnPoint.position.y, spawnPoint.position.z + randomZ);

        // Instantiate the enemy at the spawn position
        Instantiate(enemyPrefab, spawnPosition, spawnPoint.rotation);
    }
}
