using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {    
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gamer time");
            Destroy(gameObject);
        }

    }
    
}
