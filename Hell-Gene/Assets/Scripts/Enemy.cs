using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public int moveSpeed;

    public int damage;

    public Pathfinding.AIPath aipath;

    public bool hitStun;
    private float hitStunTimer;

    public Image healthBar;
    Rigidbody2D rb;

    public GameObject player;

    public int enemyType;
    private bool initialAggroTrigger;

    public int enemyVariant;
    // 0 = Lurker
    // 1 = Watcher
    // 2 = Orc
    // 3 = Imp
    // 4 = Bomber
    // 5 = Commander
    // 6 = Spitter

    public GameObject enemyBullet;
    public int bulletForce;

    public Transform attackPos; // Position of hitbox
    public float attackRange; // Range of the hitbox

    public Transform firePoint;

    public float attackTriggerRange; // Range of when enemy decides to attack.
    public float projectileTriggerRange; // Range of when enemy decides to shoot projectile
    public float detectionRange; // Range of when enemy decides to shoot projectile
    

    public bool isMelee; //categorizes enemies for melee and ranged
    private bool isFleeing; //fleeing boolean

    public LayerMask whatIsPlayer;

    public float maxAttackTime;
    public float attackTimer;
    public int knockbackForce;

    public AudioSource enemyHit;
    public AudioSource enemyKill;
    public AudioSource explode;
    float audioTimer = 0.35f;
    bool killHasPlayed = false;
    

    public bool isExploding; // For bomber

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        switch (enemyVariant) {
            case 0: // Lurker
                damage = 3;
                maxHealth = 30;
                aipath.maxSpeed = 2;
                knockbackForce = 300;
                maxAttackTime = 10f;
                break;
            case 1: // Watcher
                damage = 3;
                maxHealth = 25;
                aipath.maxSpeed = 3;
                bulletForce = 10;
                maxAttackTime = 15f;
                break;
            case 2: // Orc
                damage = 10;
                maxHealth = 60;
                aipath.maxSpeed = 1;
                knockbackForce = 700;
                maxAttackTime = 10f;
                break;
            case 3: // Imp
                damage = 5;
                maxHealth = 15;
                aipath.maxSpeed = 5;
                knockbackForce = 200;
                maxAttackTime = 1f;
                break;
            case 4: // Bomber
                damage = 20;
                maxHealth = 20;
                aipath.maxSpeed = 4;
                knockbackForce = 1500;
                maxAttackTime = 5f;
                break;
            case 5: // Commander
                damage = 15;
                maxHealth = 100;
                aipath.maxSpeed = 3;
                knockbackForce = 500;
                maxAttackTime = 10f;
                break;
            case 6: // Spitter
                damage = 10;
                maxHealth = 45;
                aipath.maxSpeed = 3;
                bulletForce = 5;
                knockbackForce = 300;
                maxAttackTime = 10f;
                break;
        }

        health = maxHealth;
        attackTimer = maxAttackTime;

        player = GameObject.FindGameObjectWithTag("Player");

        // If enemy type is of the basic passive/aggressive on trigger type (1), don't trigger pathfinding instantly.
        enemyType = 1;
        if (enemyType == 1) {
	        initialAggroTrigger = false;
	        aipath.canSearch = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isFleeing)
        {
            aipath.canSearch = false;
            aipath.canMove = false;
            Vector2 playerPos = player.transform.position;
            //Debug.Log(isFleeing);
            Vector2 fleeDir = (this.transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(fleeDir.x * moveSpeed, fleeDir.y * moveSpeed);
        }
        else
        {

            if (hitStun)
            {
                aipath.canSearch = false;
                aipath.canMove = false;
                hitStunTimer -= Time.deltaTime;
            }

            if (!hitStun && initialAggroTrigger && !isExploding)
            {
                aipath.canSearch = true;
                hitStunTimer = 1.0f;
                rb.velocity = Vector3.zero;
            }

            if (hitStunTimer <= 0)
            {
                hitStun = false;
            }
        }

        if (health <= 0)
        {  
            if (!killHasPlayed) {
                //rngesus take the wheel
                GameObject deathParticleContainer = GameObject.FindGameObjectWithTag("DeathParticle");
                GameObject deathParticle = Instantiate(deathParticleContainer, transform.position, Quaternion.identity);
                deathParticle.GetComponent<ParticleSystem>().Play();
                //particles
                RollForRandomItemDrop();

                killHasPlayed = true;
                enemyKill.Play();
            }

            audioTimer -= Time.deltaTime;
            if (audioTimer <= 0){
                Destroy(gameObject);
            }
        }

        if (Vector2.Distance(player.transform.position, transform.position) <= detectionRange && !hitStun && !isExploding) //sets destination to player location if within detection range
        {
            initialAggroTrigger = true;
            aipath.destination = player.transform.position;
        }

        if (attackTimer >= 0 && enemyVariant != 4) {
            attackTimer -= Time.deltaTime;
        }

        if (isExploding && enemyVariant == 4) {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) {
                Attack(damage);
            }
        }

        // Trigger attack
        if (Vector2.Distance(player.transform.position, transform.position) <= attackTriggerRange) {
            // Do attack logic here
            aipath.canSearch = false;
            aipath.canMove = false;
            //if enemy is a melee attacker
            if (isMelee)
            {
                Attack(damage); //attack if melee enemy
            }
            else
            {
                isFleeing = true; //flee if ranged
            }

        }

        if (Vector2.Distance(player.transform.position, transform.position) <= projectileTriggerRange)
        {
            // Do projectile logic here
            if (!isMelee)
            {
                if (attackTimer <= 0)
                {
                    aipath.canSearch = false;
                    aipath.canMove = false;
                    Fire(damage);
                }
            }
        }
        else //disable fleeing if player is outside range
        {
            isFleeing = false;
            aipath.canSearch = true;
            aipath.canMove = true;
        }
    }

    public void TakeDamage(int damage) {

        if(health >= damage) //if the enemy isnt gonna die from this hit
        {
            GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
            GameObject hitParticle = Instantiate(hitParticleContainer, transform.position, player.GetComponent<PlayerAttack>().firePoint.transform.rotation);
            hitParticle.GetComponent<ParticleSystem>().Play();

            enemyHit.Play();
        }

        int damageTaken = (int) (damage * player.GetComponent<PlayerAttack>().attackMod);
        Debug.Log("Attack Mod: " + player.GetComponent<PlayerAttack>().attackMod);
        Debug.Log("Damage Done: " + damageTaken);

        if (enemyVariant != 5)
        {
            health -= damageTaken;
        }
        else { // Commander takes halved damage
            health -= (int)(damageTaken / 2);
        }

        healthBar.fillAmount = (float)health / maxHealth;
    }

    public void Knockback(Vector3 position, int force) {

        Vector3 knockbackDir = (transform.position - position).normalized;
        hitStun = true;

        rb.AddForce(knockbackDir.normalized * force);
    }

    public void Attack(int attackDamage)
    {
        //placeholder method for enemy melee attacks
        Collider2D[] playerDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
        if (enemyVariant != 4 && attackTimer <= 0)
        {
            for (int i = 0; i < playerDamage.Length; i++)
            {
                if (playerDamage[i].gameObject.tag == "Player")
                {
                    playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, knockbackForce);

                    //particle
                    Vector3 relativePos = player.transform.position - transform.position;
                    Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                    GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                    GameObject hitParticle = Instantiate(hitParticleContainer, player.transform.position, particleRotation);
                    hitParticle.GetComponent<ParticleSystem>().Play();

                    playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                }
            }
            attackTimer = maxAttackTime;
        }

        if (enemyVariant == 4 && !isExploding) {
            isExploding = true;
            aipath.canMove = false;
            aipath.canSearch = false;
        }

        if (enemyVariant == 4 && isExploding && attackTimer <= 0) {
            
            for (int i = 0; i < playerDamage.Length; i++) {
                Debug.Log("Player Damage Length: " + playerDamage.Length);
                if (playerDamage[i].gameObject.tag == "Player")
                {
                    
                    playerDamage[i].GetComponent<PlayerMovement>().Knockback(transform.position, knockbackForce);

                    //particle
                    Vector3 relativePos = player.transform.position - transform.position;
                    Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                    GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                    GameObject hitParticle = Instantiate(hitParticleContainer, player.transform.position, particleRotation);
                    hitParticle.GetComponent<ParticleSystem>().Play();

                    playerDamage[i].GetComponent<PlayerMovement>().TakeDamage(damage);

                }
            }
            if (!killHasPlayed)
            {
                GameObject deathParticleContainer = GameObject.FindGameObjectWithTag("DeathParticle");
                GameObject deathParticle = Instantiate(deathParticleContainer, transform.position, Quaternion.identity);
                deathParticle.GetComponent<ParticleSystem>().Play();
                RollForRandomItemDrop();
                explode.Play();
                killHasPlayed = true;
            }
            audioTimer -= Time.deltaTime;
            if (audioTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Fire(int attackDamage)
    {
        //placeholder method for enemy ranged attacks
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        attackTimer = maxAttackTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Player")) {
		    //Debug.Log("Triggered!");
		    initialAggroTrigger = true;
	    }
    }

    void RollForRandomItemDrop() {
        // Random number generator
        int number = UnityEngine.Random.Range(1,101);

        //Debug.Log(number);

        if (number <= 15) {
            ItemTemplates it = GameObject.FindGameObjectWithTag("Items").GetComponent<ItemTemplates>();
            int wtRand = UnityEngine.Random.Range(0, 10);
            if(wtRand < 5) //melee
            {
                int rando = UnityEngine.Random.Range(0, it.meleeWeapons.Length);
                Instantiate(it.meleeWeapons[rando], transform.position, Quaternion.identity);
            }
            else
            {
                int rando = UnityEngine.Random.Range(0, it.rangedWeapons.Length);
                Instantiate(it.rangedWeapons[rando], transform.position, Quaternion.identity);
            }
            Debug.Log("10% drop rate!");    
        }

    }

    private void OnDrawGizmos() // Display hitboxes and ranges
    {
        Gizmos.color = Color.red; // Display hitbox
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

        Gizmos.color = Color.blue; // Display attack trigger range
        Gizmos.DrawWireSphere(transform.position, attackTriggerRange);

        Gizmos.color = Color.yellow; // Display projectile trigger range
        Gizmos.DrawWireSphere(transform.position, projectileTriggerRange);

        Gizmos.color = Color.green; // Display detection range
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
