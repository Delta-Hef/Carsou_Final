using UnityEngine;

public class CarControl : MonoBehaviour
{
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    public float motorTorque = 3000f;
    public float maxSteeringAngle = 30f;

    void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        // Apply motor torque to the back wheels
        backLeftWheelCollider.motorTorque = vInput * motorTorque;
        backRightWheelCollider.motorTorque = vInput * motorTorque;

        // Apply steering to the front wheels
        frontLeftWheelCollider.steerAngle = hInput * maxSteeringAngle;
        frontRightWheelCollider.steerAngle = hInput * maxSteeringAngle;
    }
}
