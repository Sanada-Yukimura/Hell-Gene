using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBullet : MonoBehaviour
{

    public int damage;
    public float explodeTimer;
    public int explodeRange;
    float soundTimer = 0.4f;
    bool audioHasPlayed = false;

    public LayerMask whatIsPlayer;

    public AudioSource audio;

    public GameObject bulletTrail;
    private GameObject trail;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 bAttackEuler = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().firePoint.rotation.eulerAngles;
        bAttackEuler = new Vector3(bAttackEuler.x, bAttackEuler.y, bAttackEuler.z + 180);
        trail = Instantiate(bulletTrail, transform.position, Quaternion.Euler(bAttackEuler)); //blub
        trail.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        trail.transform.position = transform.position;
        explodeTimer -= Time.deltaTime;

        if (explodeTimer <= 0)
        {
            soundTimer -= Time.deltaTime;

            if (!audioHasPlayed)
            {
                audio.Play();
                audioHasPlayed = true;
            }
            Collider2D[] playerDamage = Physics2D.OverlapCircleAll(transform.position, explodeRange, whatIsPlayer);
            for (int i = 0; i < playerDamage.Length; i++)
            {
                if (playerDamage[i].gameObject.CompareTag("Player"))
                {
                    playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, 1000);

                    //particle
                    Vector3 relativePos = collision.gameObject.transform.position - transform.position;
                    Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                    GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                    GameObject hitParticle = Instantiate(hitParticleContainer, collision.gameObject.transform.position, particleRotation);
                    hitParticle.GetComponent<ParticleSystem>().Play();

                    playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                }
            }

            GameObject deathParticleContainer = GameObject.FindGameObjectWithTag("DeathParticle");
            GameObject deathParticle = Instantiate(deathParticleContainer, transform.position, Quaternion.identity);
            deathParticle.GetComponent<ParticleSystem>().Play();

            if (explodeTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemycollider" && collision.gameObject.tag != "Enemy")
        {
            soundTimer -= Time.deltaTime;
            Collider2D[] playerDamage = Physics2D.OverlapCircleAll(transform.position, explodeRange, whatIsPlayer);
            for (int i = 0; i < playerDamage.Length; i++)
            {
                if (playerDamage[i].gameObject.CompareTag("Player"))
                {
                    playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, 1000);

                    //particle
                    Vector3 relativePos = collision.gameObject.transform.position - transform.position;
                    Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                    GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                    GameObject hitParticle = Instantiate(hitParticleContainer, collision.gameObject.transform.position, particleRotation);
                    hitParticle.GetComponent<ParticleSystem>().Play();

                    playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                }
            }

            GameObject deathParticleContainer = GameObject.FindGameObjectWithTag("DeathParticle");
            GameObject deathParticle = Instantiate(deathParticleContainer, transform.position, Quaternion.identity);
            deathParticle.GetComponent<ParticleSystem>().Play();

            if (!audioHasPlayed)
            {
                audio.Play();
                audioHasPlayed = true;
            }
            if (soundTimer <= 0)
            {
                Destroy(trail);
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(trail);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() // Debug hitboxes
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRange);

        //Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }
}
