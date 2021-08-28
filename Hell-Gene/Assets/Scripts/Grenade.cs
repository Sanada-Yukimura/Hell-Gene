using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;
    public float explodeTimer;
    public int explodeRange;
    public GameObject explosionParticles;
    float soundTimer = 0.5f;

    public AudioSource audio;

    void Update()
    {
        explodeTimer -= Time.deltaTime;
        if (explodeTimer < 0) {
            audio.Play();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (explodeTimer <= 0)
        {
            soundTimer -= Time.deltaTime;
            if (collision.GetType() == typeof(CircleCollider2D) && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (collision.GetType() == typeof(CircleCollider2D) && collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<Boss>().TakeDamage(damage);
            }

            //particle
            
            GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity); //megumin
            explosion.GetComponent<ParticleSystem>().Play();

            if (soundTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() // Debug hitboxes
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4);

        //Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }
}
