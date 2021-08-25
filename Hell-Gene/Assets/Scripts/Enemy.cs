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

    private int enemyType;
    private bool initialAggroTrigger;

    public Transform attackPos; // Position of hitbox
    public float attackRange; // Range of the hitbox

    public float attackTriggerRange; // Range of when enemy decides to attack.
    public float projectileTriggerRange; // Range of when enemy decides to shoot projectile
    public float detectionRange; // Range of when enemy decides to shoot projectile

    public bool isMelee; //categorizes enemies for melee and ranged
    private bool isFleeing; //fleeing boolean

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;

        damage = 10;

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
            Debug.Log(isFleeing);
            Vector2 fleeDir = (this.transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(fleeDir.x * moveSpeed, fleeDir.y * moveSpeed);
        }
        else
        {
            if (initialAggroTrigger)
            {
                aipath.canSearch = true;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }

            if (hitStun)
            {
                aipath.canSearch = false;
                hitStunTimer -= Time.deltaTime;
            }

            if (!hitStun && initialAggroTrigger)
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

        if(Vector2.Distance(player.transform.position, transform.position) <= detectionRange) //sets destination to player location if within detection range
        {
            initialAggroTrigger = true;
            aipath.destination = player.transform.position;
        }

        // Trigger attack
        if (Vector2.Distance(player.transform.position, transform.position) <= attackTriggerRange) {
            // Do attack logic here
            aipath.canSearch = false;
            //if enemy is a melee attacker
            if (isMelee)
            {
                Attack(1); //attack if melee enemy
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
                aipath.canSearch = false;
                aipath.canMove = false;
                Fire(1);
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
        health -= damage;

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
    }

    public void Fire(int attackDamage)
    {
        //placeholder method for enemy ranged attacks
    }

    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Player")) {
		    //Debug.Log("Triggered!");
		    initialAggroTrigger = true;
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
