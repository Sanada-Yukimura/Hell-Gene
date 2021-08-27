using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBullet : MonoBehaviour
{

    public int damage;
    public float explodeTimer;
    public int explodeRange;

    public LayerMask whatIsPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        explodeTimer -= Time.deltaTime;

        if (explodeTimer <= 0)
        {
            Collider2D[] playerDamage = Physics2D.OverlapCircleAll(transform.position, explodeRange, whatIsPlayer);
            for (int i = 0; i < playerDamage.Length; i++)
            {
                if (playerDamage[i].gameObject.CompareTag("Player"))
                {
                    playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, 1000);
                    playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemycollider" && collision.gameObject.tag != "Enemy")
        {
            Collider2D[] playerDamage = Physics2D.OverlapCircleAll(transform.position, explodeRange, whatIsPlayer);
            for (int i = 0; i < playerDamage.Length; i++)
            {
                if (playerDamage[i].gameObject.CompareTag("Player"))
                {
                    playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, 1000);
                    playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                }
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
        Gizmos.DrawWireSphere(transform.position, explodeRange);

        //Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }
}
