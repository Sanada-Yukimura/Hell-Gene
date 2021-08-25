using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    public float projectileRange;
    public float projectileSafety;

    public Transform player;
    public Pathfinding.AIPath aipath;

    Rigidbody2D rb;

    public bool isFlipped;
    public bool isAttacking;

    public float maxChargeTime = 0.8f; // Telegraph boss attack
    public float maxAttackTime = 0.3f; // Attack animation
    public float maxCooldownTime = 1.0f; // Pause
    public float maxAttackBuffer = 10f; // Buffer before boss is able to attack again
    private float chargeTime;
    private float attackTime;
    private float cooldownTime;
    public float attackBuffer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        phase = 0;

        chargeTime = maxChargeTime;
        attackTime = maxAttackTime;
        cooldownTime = maxCooldownTime;
        attackBuffer = maxAttackBuffer;

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

        if (attackBuffer >= 0) {
            attackBuffer -= Time.deltaTime;
        }

        if (isAttacking) {
            aipath.canSearch = false;
            aipath.canMove = false;

            // Telegraph attack
            if (chargeTime >= 0) {
                chargeTime -= Time.deltaTime;
                Debug.Log("Telegraphing Attack");
            }

            // Attack
            if (chargeTime <= 0 && attackTime >= 0) {
                attackTime -= Time.deltaTime;
                Debug.Log("Attacking");
            }

            // Cooldown
            if (attackTime <= 0 && cooldownTime >= 0) {
                cooldownTime -= Time.deltaTime;
                Debug.Log("Cooling Down");
            }

            // Return to move
            if (cooldownTime <= 0) {
                isAttacking = false;
                chargeTime = maxChargeTime;
                attackTime = maxAttackTime;
                cooldownTime = maxCooldownTime;
                attackBuffer = maxAttackBuffer;
                aipath.canSearch = true;
                aipath.canMove = true;
            }

        }



    }

    public void LookAtPlayer() {

        if (transform.position.y > player.position.y && isFlipped && !isAttacking)
        {
            pivot.Rotate(0f, 0f, 180f);
            isFlipped = false;
        }
        else if (transform.position.y < player.position.y && !isFlipped && !isAttacking) {
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, projectileRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, projectileSafety);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (phase == 0 || phase == 2)
        {
            if (other.CompareTag("Player"))
            {
                if (!isAttacking && attackBuffer <= 0)
                {
                    isAttacking = true;
                }
            }
        }
    }
}
