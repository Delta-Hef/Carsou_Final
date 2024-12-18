using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // References to player and car cameras
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera carCamera;

    public void SwitchToCarCamera()
    {
        if (carCamera != null && playerCamera != null)
        {
            // Switch camera priorities
            playerCamera.Priority = 0;
            carCamera.Priority = 20;
        }
        else
        {
            Debug.LogError("Camera references are missing.");
        }
    }

    public void SwitchToPlayerCamera()
    {
        if (carCamera != null && playerCamera != null)
        {
            // Switch camera priorities
            playerCamera.Priority = 20;
            carCamera.Priority = 0;
        }
        else
        {
            Debug.LogError("Camera references are missing.");
        }
    }
}
