using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;    // Reference to the player's position
    public float moveSpeed = 3f;  // Speed at which the enemy moves toward the player
    public float followDistance = 20f;  // Distance at which the enemy starts following the player

    private void Update()
    {
        // If there is no player reference, do nothing
        if (player == null)
            return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the enemy is within the follow range, move toward the player
        if (distanceToPlayer <= followDistance)
        {
            // Move towards the player using the moveSpeed
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(player); // Rotate to face the player
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy was hit by a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Optional: Log to make sure the enemy is destroyed properly
        Debug.Log("Enemy destroyed: " + gameObject.name);
    }
}
