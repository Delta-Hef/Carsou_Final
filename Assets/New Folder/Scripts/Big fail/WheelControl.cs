using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel; // This should be the paired mesh (Front_Wheels or Back_Wheels)

    Vector3 position;
    Quaternion rotation;
    WheelCollider wheelCollider;

    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        // Get the WheelCollider's world position and rotation
        wheelCollider.GetWorldPose(out position, out rotation);

        // Update the wheel model's transform
        if (wheelModel != null)
        {
            wheelModel.position = position;
            wheelModel.rotation = rotation;
        }
    }
}
