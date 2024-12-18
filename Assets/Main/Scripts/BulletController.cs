//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BulletController : MonoBehaviour
//{

//    [SerializeField]

//    private GameObject bulletDecal;


//    private float speed = 50f;
//    private float timeToDestroy = 3f;

//    public Vector3 target { get; set; }
//    public bool hit { get; set; }
//    // Start is called before the first frame update
//    void OnEnable()
//    {
//        Destroy(gameObject, timeToDestroy);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
//        if (!hit && Vector3.Distance(transform.position, target) < 0.1f)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void OnCollisionEnter(Collision other)
//    {

//        if (other.gameObject.CompareTag("Enemy"))
//            return;


//        ContactPoint contact = other.GetContact(0);
//        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
//        Destroy(gameObject);

//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;  // Bullet hole effect

    private float speed = 50f;        // Bullet speed
    private float timeToDestroy = 3f; // Time before bullet is destroyed

    public Vector3 target { get; set; }   // Target point
    public bool hit { get; set; }          // Whether the bullet has hit something

    // Called when the bullet is enabled
    void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        // Move bullet toward target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Destroy bullet when it reaches the target
        if (!hit && Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // Called when the bullet collides with something
    private void OnCollisionEnter(Collision other)
    {
        // Debugging: log the name of the object the bullet collided with
        Debug.Log("Bullet hit: " + other.gameObject.name);

        // If bullet hits the enemy, destroy both the bullet and the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject);       // Destroy the bullet
            Debug.Log("Enemy destroyed!");
        }
        else
        {
            // If bullet hits anything else (e.g., wall), create a bullet hole
            ContactPoint contact = other.GetContact(0);
            GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
            Destroy(gameObject); // Destroy the bullet after impact
        }
    }
}
