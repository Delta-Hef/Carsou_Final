using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI Malustext;


    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    [SerializeField] private float JumpForce = 0.5f;
    //[SerializeField] private float maxHeight = 20f; // Max height limit for the car
    [SerializeField] private float downwardForce = 5f;
    [SerializeField] private float stabilizingForce = 500f;
    [SerializeField] private AudioClip Klaxon;
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private Rigidbody rb;
    private AudioSource audioo;
    private int count;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioo = GetComponent<AudioSource>();
        count = 0;
        Malustext.text = "";
    }
    void Update()
    {
        GetInput();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F) && IsGrounded())
        {
            Jump();
        }

        // Honk the horn when the player presses H
        if (Input.GetKeyDown(KeyCode.H))
        {
            HonkHorn();
        }

        if (Vector3.Dot(transform.up, Vector3.up) < 0.01f) // 
        {
            ResetCarOrientation();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetCarOrientation();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            Jump();
        }

        if (other.gameObject.CompareTag("Piege"))
        {
            other.gameObject.SetActive(false);
            Malus();
            count = count - 2;
            SetCountText();

        }
    }

    void Malus()
    {
        StartCoroutine(ShowMalusText());
    }

    private IEnumerator ShowMalusText()
    {
        Malustext.text = "CAREFUL!";


        // WaitING
        yield return new WaitForSeconds(1.3f);

        Malustext.text = "";
    }
    void SetCountText()
    {
        countText.text = "Points: " + count.ToString();
        if (count >= 7)
        {
            // winTextObject.SetActive(true);
        }

        //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        ApplyStabilizingForce();
    }

    private void GetInput()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");

        
        verticalInput = Input.GetAxis("Vertical");

        
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        // Update wheel position
        wheelTransform.position = pos;

        wheelTransform.rotation = rot * Quaternion.Euler(0, -90, 0); // Adjust this value if needed
    }

    private void ResetCarOrientation()
    {
        // Set the car's position slightly above the ground to avoid it getting stuck
        transform.position += Vector3.up * 0.5f;

        // Reset the rotation to align the car upright
        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        // Reset velocity to avoid immediate flipping again
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


    private void Jump()
    {
        rb.AddForce(transform.forward * JumpForce, ForceMode.Impulse);
    }

    // Function to play the horn sound
    private void HonkHorn()
    {
        if (Klaxon != null && audioo != null)
        {
            audioo.PlayOneShot(Klaxon);
        }
    }

    // Check if the car is grounded
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }
    private void ApplyDownwardForce()
    {
        if (!IsGrounded())
        {
            // Apply a downward force to keep the car grounded better
            rb.AddForce(Vector3.down * downwardForce, ForceMode.Acceleration);
        }
    }

    private void ApplyStabilizingForce()
    {
        // Calculate the inverse downward force
        Vector3 downwardForce = -transform.up * stabilizingForce;

        // Apply it at the car's center of mass to counter flipping
        rb.AddForce(downwardForce, ForceMode.Acceleration);

        // Add angular damping to reduce rotation in mid-air
        rb.angularVelocity *= 0.95f;  // Adjust for damping intensity
    }
}