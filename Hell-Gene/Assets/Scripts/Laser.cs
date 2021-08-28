using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damageInterval;
    public float despawnTimer;
    public int damage;
    public GameObject laserParticles;

    void Start()
    {
        //particle
        GameObject laser = Instantiate(laserParticles, transform.position, transform.rotation); //ekusukalibaaa
        laser.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0) Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemycollider")
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
        }

        if (collision.gameObject.tag == "BossCollider")
        {
            collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
