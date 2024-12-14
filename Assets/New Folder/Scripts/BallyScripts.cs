using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallyScripts : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 2f;
    public TextMeshProUGUI countText;
    private int count;
    public GameObject winTextObject;
    private AudioSource a;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        a = GetComponent<AudioSource>();
    }

   void OnMove(InputValue mv)
    {
        Vector2 movementVector = mv.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 7)
        {
            winTextObject.SetActive(true);
        }

        //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    private void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement*speed);
    }

     void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;
            SetCountText();
            a.Play();
        }

        if (other.gameObject.CompareTag("Piege"))
        {
            other.gameObject.SetActive(false);

            count = count - 1;
            SetCountText();
            a.Play();
        }

        if (other.gameObject.CompareTag("Bonus"))
        {
            other.gameObject.SetActive(false);

            count = count + 5;
            SetCountText();
            a.Play();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You lost bro";
            
        }
    }
}
