using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Bally;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Bally.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Bally)
            transform.position = Bally.transform.position + offset;
    }
}
