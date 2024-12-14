using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{
    public Transform WallTransform;
    public float MoveDistance = 5f;
    public float MoveSpeed = 1f;
    public float ApproachDistance = 5f;

    private Vector3 initialPosition;

    private void Start()
    {
        // Store the wall's initial position
        initialPosition = WallTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(MoveWallUp());
        }
    }

    private IEnumerator MoveWallUp()
    {
        Vector3 targetPosition = initialPosition + Vector3.up * MoveDistance;

        while (Vector3.Distance(WallTransform.position, targetPosition) > 0.01f)
        {
            WallTransform.position = Vector3.MoveTowards(WallTransform.position, targetPosition, MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
