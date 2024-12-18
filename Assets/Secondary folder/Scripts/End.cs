using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
     
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            texto.text = "Thank You For Playing :)" + "\n"+
                "Press r to restart";
        }

        Time.timeScale = 0; 








    }
}
