using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float damage;
    public float explodeTimer;

    void Update()
    {
        explodeTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (explodeTimer <= 0)
        {
            if (collision.GetType() == typeof(CircleCollider2D) && collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Boom");
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() // Debug hitboxes
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);

        //Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }
}
