using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Transform target; // The object to orbit around (car)
    public float rotationSpeed = 10f; // Rotation speed

    void Start()
    {
        Time.timeScale = 0.2f; // Slow motion effect
    }


    void Update()
    {
        // Rotate the CameraRig around the target object
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Keep the camera facing the target
        transform.LookAt(target);
    }
}
