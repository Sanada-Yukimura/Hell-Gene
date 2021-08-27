using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float damage = 15f;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
            collision.gameObject.GetComponent<PlayerMovement>().Knockback(transform.position, 150 );
            Destroy(gameObject);
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
