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

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;

        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (hitStun) {
            aipath.canSearch = false;
            hitStunTimer -= Time.deltaTime;
        }

        if (!hitStun) {
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
}
