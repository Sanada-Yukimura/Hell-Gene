using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decomposer : MonoBehaviour
{
    public int damage;
    public GameObject bulletPrefab;
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
        if (collision.gameObject.tag == "Enemycollider")
        {
            int enemyHealth = collision.gameObject.GetComponentInParent<Enemy>().health;
            if (enemyHealth <= damage) //spawns extra bullets if the collision will kill
            {
                //up bullet
                GameObject uBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D uBulletBody = uBullet.GetComponent<Rigidbody2D>();
                uBulletBody.AddForce(transform.up * 15, ForceMode2D.Impulse);

                //right bullet
                GameObject rBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D rBulletBody = rBullet.GetComponent<Rigidbody2D>();
                rBulletBody.AddForce(transform.right * 15, ForceMode2D.Impulse);

                //down bullet
                GameObject dBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D dBulletBody = dBullet.GetComponent<Rigidbody2D>();
                dBulletBody.AddForce(-transform.up * 15, ForceMode2D.Impulse);

                //left bullet
                GameObject lBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D lBulletBody = lBullet.GetComponent<Rigidbody2D>();
                lBulletBody.AddForce(-transform.right * 15, ForceMode2D.Impulse);
            }
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Destroy(trail);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "BossCollider")
        {
            int enemyHealth = collision.gameObject.GetComponentInParent<Boss>().health;
            if (enemyHealth <= damage) //spawns extra bullets if the collision will kill
            {
                //up bullet
                GameObject uBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D uBulletBody = uBullet.GetComponent<Rigidbody2D>();
                uBulletBody.AddForce(transform.up * 15, ForceMode2D.Impulse);

                //right bullet
                GameObject rBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D rBulletBody = rBullet.GetComponent<Rigidbody2D>();
                rBulletBody.AddForce(transform.right * 15, ForceMode2D.Impulse);

                //down bullet
                GameObject dBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D dBulletBody = dBullet.GetComponent<Rigidbody2D>();
                dBulletBody.AddForce(-transform.up * 15, ForceMode2D.Impulse);

                //left bullet
                GameObject lBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D lBulletBody = lBullet.GetComponent<Rigidbody2D>();
                lBulletBody.AddForce(-transform.right * 15, ForceMode2D.Impulse);
            }
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
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
