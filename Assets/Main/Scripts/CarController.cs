using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;
    public Transform carSpawnPoint;

    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [SerializeField] private GameObject player; // Reference to player
    [SerializeField] private GameObject Carsou;
    [SerializeField] private CinemachineVirtualCamera carCamera; // Car camera
    [SerializeField] private Transform exitPoint; // Where the player exits the car
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private Rigidbody rb;
    private AudioSource audioo;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioo = GetComponent<AudioSource>();
        Debug.Log("CarController initialized.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reloading the scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attempting to exit the car.");
            ExitCar();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            RedropCar();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);

        }

        // Get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isBreaking = Input.GetKey(KeyCode.Space);

        // Debug the input values
        Debug.Log($"Horizontal: {horizontalInput}, Vertical: {verticalInput}, IsBreaking: {isBreaking}");
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            HandleMotor();
            HandleSteering();
            //ApplyDownwardForce();
            //ApplyStabilizingForce();

        }
        else
        {
            Debug.LogError("Rigidbody is not assigned to the car.");
        }
    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) // Space key for jump
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            Debug.Log("Jumping!");
        }
    }


    void RedropCar()
    {
        // Reset the car's position to the spawn point
        transform.position = carSpawnPoint.position;

        // Reset the car's rotation to match the spawn point (no predefined rotation)
        transform.rotation = carSpawnPoint.rotation;

        // Reset the rigidbody velocity and angular velocity to avoid unwanted movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Enable gravity by making sure the Rigidbody is using gravity
        rb.useGravity = true;

        // Optionally, you can add a slight force to "push" the car down immediately
        // This will help it start falling right away
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // You can adjust the force to suit your needs

        Debug.Log("Car has been redropped!");
    }



    private void HandleMotor()
    {
        if (frontLeftWheelCollider != null && frontRightWheelCollider != null)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            
            currentBreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
            Debug.Log("Handling motor. Motor torque applied.");
        }
        else
        {
            Debug.LogError("Wheel Colliders are missing!");
        }
    }

    private void ApplyBreaking()
    {
        if (frontLeftWheelCollider != null && frontRightWheelCollider != null)
        {
            currentBreakForce = isBreaking ? breakForce : 0f;

            // Apply brake torque to all wheels
            frontRightWheelCollider.brakeTorque = currentBreakForce;
            frontLeftWheelCollider.brakeTorque = currentBreakForce;
            rearLeftWheelCollider.brakeTorque = currentBreakForce;
            rearRightWheelCollider.brakeTorque = currentBreakForce;

            if (isBreaking)
            {
                // Add drag to simulate stronger braking
                rb.drag = 1f;
            }
            else
            {
                rb.drag = 0f;
            }

            Debug.Log($"Applying brakes. Brake Force: {currentBreakForce}");
        }
        else
        {
            Debug.LogError("Wheel Colliders are missing for breaking.");
        }
    }


    private void HandleSteering()
    {
        if (frontLeftWheelCollider != null && frontRightWheelCollider != null)
        {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
            Debug.Log("Handling steering.");
        }
        else
        {
            Debug.LogError("Wheel Colliders are missing for steering.");
        }
    }

    private void ApplyDownwardForce()
    {
        if (!IsGrounded())
        {
            rb.AddForce(Vector3.down * 5f, ForceMode.Acceleration);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    private void ApplyStabilizingForce()
    {
        Vector3 downwardForce = transform.forward * 500f;
        rb.AddForce(downwardForce, ForceMode.Acceleration);
    }

    // Method to handle exiting the car
    public void ExitCar()
    {
        Debug.Log("Exiting car...");

        if (player != null && exitPoint != null)
        {
            // Move the player to the exit point
            player.transform.position = exitPoint.position;
            player.SetActive(true);  // Reactivate the player

            // Disable car controls
            CarController carController = Carsou.GetComponent<CarController>();
            if (carController != null)
            {
                carController.enabled = false;
                Debug.Log("CarController disabled.");
            }

            // Switch back to player camera
            if (carCamera != null && playerCamera != null)
            {
                carCamera.Priority = 0;
                playerCamera.Priority = 20;
                Debug.Log("Camera switched to player.");
            }
            else
            {
                Debug.LogError("Cameras are missing for switching.");
            }
        }
        else
        {
            Debug.LogError("Player or exit point is not assigned!");
        }
    }
}
