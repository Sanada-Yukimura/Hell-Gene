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

    public int damage;

    public Pathfinding.AIPath aipath;

    public bool hitStun;
    private float hitStunTimer;

    public Image healthBar;
    Rigidbody2D rb;

    public Transform player;

    private int enemyType;
    private bool initialAggroTrigger;

    public Transform attackPos; // Position of hitbox
    public float attackRange; // Range of the hitbox

    public float attackTriggerRange; // Range of when enemy decides to attack.
    public float projectileTriggerRange; // Range of when enemy decides to shoot projectile

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;

        damage = 10;
		
        
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
	    if (initialAggroTrigger) {
		    aipath.canSearch = true;
	    }

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (hitStun) {
            aipath.canSearch = false;
            hitStunTimer -= Time.deltaTime;
        }

        if (!hitStun && initialAggroTrigger) {
            aipath.canSearch = true;
            hitStunTimer = 1.0f;
            rb.velocity = Vector3.zero;
        }

        if (hitStunTimer <= 0) {
            hitStun = false;
        }


        // Trigger attack
        if (Vector2.Distance(player.transform.position, transform.position) <= attackTriggerRange) { 
            // Do attack logic here
        }

        if (Vector2.Distance(player.transform.position, transform.position) <= projectileTriggerRange) { 
            // Do projectile logic here
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
    }
}
