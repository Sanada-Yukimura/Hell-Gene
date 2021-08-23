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

    private int enemyType;
    private bool initialAggroTrigger;

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
		    Debug.Log("Triggered!");
		    initialAggroTrigger = true;
	    }
    }
    
    
}
