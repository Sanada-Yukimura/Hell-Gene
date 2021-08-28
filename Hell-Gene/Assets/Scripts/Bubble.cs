using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float damageInterval;
    public float despawnTimer;
    public int damage;
    private bool firstProc = false;
    public GameObject bubbleParticles;
    private GameObject bubble;

    void Start()
    {
        //particle
        bubble = Instantiate(bubbleParticles, transform.position, Quaternion.identity); //blub
        bubble.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        bubble.transform.position = transform.position; //move particles
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(bubble);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemycollider")
        {
            if (despawnTimer % damageInterval <= 0.033 || !firstProc) collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            firstProc = true;
        }

        if (collision.gameObject.tag == "BossCollider")
        {
            if (despawnTimer % damageInterval <= 0.033 || !firstProc) collision.gameObject.GetComponentInParent<Boss>().TakeDamage(damage);
            firstProc = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
