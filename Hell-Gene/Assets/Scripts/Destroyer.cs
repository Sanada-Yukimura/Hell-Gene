using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Spawnpoint" || collider.gameObject.tag == "Module")
        {
            Destroy(collider.gameObject);
        }
    }
}
