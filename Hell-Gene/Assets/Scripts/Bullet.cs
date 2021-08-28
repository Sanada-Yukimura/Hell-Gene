using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemycollider") {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "BossCollider") {
            collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) Destroy(gameObject);

        if (collision.gameObject.tag == "BossCollider")
        {
            collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemycollider")
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
