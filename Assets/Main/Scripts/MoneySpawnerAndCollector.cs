using UnityEngine;
using TMPro;

public class MoneySpawnerAndCollector : MonoBehaviour
{
    [Header("Money Settings")]
    public GameObject moneyPrefab;  // The money prefab (coin, dollar, etc.)
    public Transform spawnPoint;    // The spawn point from where money will appear
    public float spawnRadius = 10f; // The radius within which money will spawn around the spawn point
    public int moneyAmount = 5;     // How many pieces of money to spawn at once

    [Header("Player Settings")]
    public GameObject Player;       // Reference to the car GameObject
    public int currentMoney = 0;    // The player's current amount of money

    [Header("UI Settings")]
    public TextMeshProUGUI moneyText; // Optional: UI Text to display the player's money

    void Start()
    {
        // Spawn money at the start
        SpawnMoney();
    }

    // Update UI text to reflect the player's current money
    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + currentMoney.ToString();
        }
    }

    // Spawn money at random positions within the spawn radius
    void SpawnMoney()
    {
        for (int i = 0; i < moneyAmount; i++)
        {
            // Generate random position within the spawn radius
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(
                spawnPoint.position.x + randomCircle.x,
                spawnPoint.position.y,  // Ensure the money spawns at the same height as the spawn point
                spawnPoint.position.z + randomCircle.y
            );

            // Instantiate the money prefab at the generated position
            GameObject newMoney = Instantiate(moneyPrefab, spawnPosition, Quaternion.identity);

            // Set the money's collider to trigger mode to detect collisions
            Collider moneyCollider = newMoney.GetComponent<Collider>();
            if (moneyCollider != null)
            {
                moneyCollider.isTrigger = true;
            }

            // Attach a MoneyScript for collection detection
            MoneyScript moneyScript = newMoney.AddComponent<MoneyScript>();
            moneyScript.moneyValue = 10;  // Set the value of the money item
            moneyScript.moneyManager = this;  // Assign this manager for handling money collection
            moneyScript.Player = Player;     // Assign the Player (car) reference
        }
    }

    // Add money to the player's total
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyDisplay();
    }
}

public class MoneyScript : MonoBehaviour
{
    public int moneyValue = 10;  // Value of this money item
    public MoneySpawnerAndCollector moneyManager;  // Reference to the MoneySpawnerAndCollector script
    public GameObject Player;  // Reference to the car GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding GameObject is the Player (car)
        if (other.gameObject == Player)
        {
            // Call the method to collect the money
            CollectMoney();
        }
    }

    private void CollectMoney()
    {
        // Increase the player's money
        if (moneyManager != null)
        {
            moneyManager.AddMoney(moneyValue);  // Increase money by the defined value
        }

        // Destroy the money object after collection
        Destroy(gameObject);
    }
}
