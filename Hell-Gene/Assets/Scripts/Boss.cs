using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossLevel = 0;
    public int maxHealth;
    public int health;
    public int damage;

    int phase = 0;

    public Transform attackPos;
    public float attackRange;
    public Transform pivot;
    public Transform firePoint;

    public Transform player;

    Rigidbody2D rb;

    public bool isFlipped;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        phase = 0;


        isFlipped = false;

        switch(bossLevel) {
            case 0:
                maxHealth = 500;
                damage = 10;

                break;
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Phase calculations
        if (((float)health / maxHealth) >= 0.67)
        {
            phase = 0;
        }
        else if (((float)health / maxHealth) <= 0.67 && ((float)health / maxHealth) > 0.33)
        {
            phase = 1;
        }
        else {
            phase = 2;
        }

        // Flip to player based on y
        LookAtPlayer();
        
    }

    private void FixedUpdate()
    {

    }

    void LookAtPlayer() {
        if (transform.position.y > player.position.y && isFlipped)
        {
            pivot.Rotate(0f, 0f, 180f);
            isFlipped = false;

        }
        else if (transform.position.y < player.position.y && !isFlipped) {
            pivot.Rotate(0f, 0f, 180f);
            isFlipped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //healthBar.fillAmount = (float)health / maxHealth;
    }

    public void Knockback(Vector3 position, int force)
    {
        Vector3 knockbackDir = (transform.position - position).normalized;

        if (!isAttacking)
        {
            rb.AddForce(knockbackDir.normalized * force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
