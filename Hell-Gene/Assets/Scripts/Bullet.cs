using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public GameObject bulletTrail;
    private GameObject trail;

    void Start()
    {
        Vector3 pAttackEuler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().firePoint.rotation.eulerAngles;
        pAttackEuler = new Vector3(pAttackEuler.x, pAttackEuler.y, pAttackEuler.z + 180);
        trail = Instantiate(bulletTrail, transform.position, Quaternion.Euler(pAttackEuler)); //blub
        trail.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        trail.transform.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemycollider") {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Destroy(trail);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "BossCollider") {
            collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
            Destroy(trail);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 6)
        {
            Destroy(trail);
            Destroy(gameObject); //obstacle layer
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(trail);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "BossCollider")
        {
            collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
            Destroy(trail);
        }

        if (collision.gameObject.tag == "Enemycollider")
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            Destroy(trail);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(trail);
        Destroy(gameObject);
    }
}
