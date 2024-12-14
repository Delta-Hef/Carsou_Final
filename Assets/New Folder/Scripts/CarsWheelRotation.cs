using UnityEngine;

public class CarWheelRotation : MonoBehaviour
{
    // Assign the tire transforms for each pair
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform backLeftWheel;
    public Transform backRightWheel;

    // Speed of wheel rotation
    public float wheelRotationSpeed = 500f;

    void Update()
    {
        // Rotate both pairs of tires
        RotateWheel(frontLeftWheel);
        RotateWheel(frontRightWheel);
        RotateWheel(backLeftWheel);
        RotateWheel(backRightWheel);
    }

    void RotateWheel(Transform wheel)
    {
        // Rotate each tire around its local X-axis (to simulate rotation)
        wheel.Rotate(Vector3.right * wheelRotationSpeed * Time.deltaTime);
    }
}
