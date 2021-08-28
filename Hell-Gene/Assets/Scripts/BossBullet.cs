using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 15;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
            Destroy(gameObject);
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
