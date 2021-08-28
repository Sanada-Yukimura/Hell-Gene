using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 15;
    public GameObject bulletTrail;
    private GameObject trail;

    void Start()
    {
        Vector3 bAttackEuler = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().firePoint.rotation.eulerAngles;
        bAttackEuler = new Vector3(bAttackEuler.x, bAttackEuler.y, bAttackEuler.z + 180);
        trail = Instantiate(bulletTrail, transform.position, Quaternion.Euler(bAttackEuler)); //blub
        trail.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        trail.transform.position = transform.position;
    }

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

            Destroy(trail);
            Destroy(gameObject);
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(trail);
        Destroy(gameObject);
    }
}
