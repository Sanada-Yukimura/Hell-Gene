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

            //particle
            Vector3 relativePos = collision.gameObject.transform.position - transform.position;
            Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
            GameObject hitParticle = Instantiate(hitParticleContainer, collision.gameObject.transform.position, particleRotation);
            hitParticle.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject);
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
