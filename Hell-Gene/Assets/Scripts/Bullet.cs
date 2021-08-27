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
        }

        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy") Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
