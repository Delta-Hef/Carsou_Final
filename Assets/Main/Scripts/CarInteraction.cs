using UnityEngine;
using TMPro;

public class CarInteraction : MonoBehaviour
{
    public GameObject player;  // Reference to player GameObject
    public GameObject Carsou;  // Reference to car GameObject
    public TextMeshProUGUI interactionPrompt;  // UI prompt for "Press E"
    public CameraManager cameraManager;  // Reference to CameraManager
    public GameObject carPrefab; // Prefab of the car to respawn
    private GameObject currentCar;

    private bool isNearCar = false;

    private void Start()
    {
        interactionPrompt.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCar = true;
            interactionPrompt.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCar = false;
            interactionPrompt.enabled = false;
        }
    }

    private void Update()
    {
        if (isNearCar && Input.GetKeyDown(KeyCode.E))
        {
            EnterCar();
        }
    }

    void EnterCar()
    {
        // Null checks for player and Carsou GameObjects
        if (player == null)
        {
            Debug.LogError("Player reference is missing! Please assign the Player GameObject.");
            return;
        }
        if (Carsou == null)
        {
            Debug.LogError("Carsou reference is missing! Please assign the Car GameObject.");
            return;
        }

        // Deactivate player and enable car controls
        player.SetActive(false);
        CarController carController = Carsou.GetComponent<CarController>();
        if (carController != null)
        {
            Debug.Log("CarController found. Enabling car controls...");

            carController.enabled = true;  // Enable car controls
            Rigidbody rb = Carsou.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Rigidbody found. Setting up...");
                rb.isKinematic = false;  // Ensure the Rigidbody is not kinematic (required for physics-based movement)
                rb.constraints = RigidbodyConstraints.None;  // Remove any constraints
                Debug.Log("Rigidbody is set up correctly.");
            }
            else
            {
                Debug.LogError("Rigidbody not found on car object.");
            }
            cameraManager.SwitchToCarCamera();  // Switch to car camera
        }
        else
        {
            Debug.LogError("CarController component is missing on Carsou! Please add it.");
        }

        interactionPrompt.enabled = false;
    }

    void ExitCar()
    {
        // Reactivate the player and disable car controls
        player.SetActive(true);
        CarController carController = Carsou.GetComponent<CarController>();
        if (carController != null)
        {
            carController.enabled = false;  // Disable car controls
            cameraManager.SwitchToPlayerCamera();  // Switch back to player camera
        }
        else
        {
            Debug.LogError("CarController component is missing on Carsou! Please add it.");
        }
    }
}
