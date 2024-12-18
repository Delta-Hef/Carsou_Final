using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerCar : MonoBehaviour
{
    public GameObject car;      
    private Vector3 offset;     

    void Start()
    {
        offset = transform.position - car.transform.position;
    }

    void LateUpdate()
    {
        if (car)
        {
            // Smoothly update the position
            transform.position = Vector3.Lerp(transform.position, car.transform.position + offset, Time.deltaTime * 5f);

            // Smoothly update the rotation
            Quaternion targetRotation = Quaternion.LookRotation(car.transform.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }


}
