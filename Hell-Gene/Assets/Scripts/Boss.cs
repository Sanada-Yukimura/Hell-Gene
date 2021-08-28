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
    public int knockback;

    int phase = 0;

    public Transform attackPos;
    public float attackRange;
    public Transform pivot;
    public Transform firePoint;
    public LayerMask whatIsPlayer;

    public float projectileRange;
    public float projectileSafety;
    public float projectileBuffer;
    public int bulletForce;

    public Transform player;
    public Pathfinding.AIPath aipath;
    public Pathfinding.AIDestinationSetter destinationSetter;

    public GameObject bossBullet;

    public SpriteRenderer childSprite;

    public Sprite bossDown;
    public Sprite bossUp;

    Rigidbody2D rb;

    public bool isFlipped;
    public bool isAttacking;

    public float maxChargeTime = 0.8f; // Telegraph boss attack
    public float maxAttackTime = 0.2f; // Attack animation
    public float maxCooldownTime = 1.0f; // Pause
    public float maxAttackBuffer = 10f; // Buffer before boss is able to attack again
    private float chargeTime;
    private float attackTime;
    private float cooldownTime;
    public float attackBuffer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        destinationSetter.target = player;

        rb = GetComponent<Rigidbody2D>();
        phase = 0;

        aipath.maxSpeed = 1;

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
                Collider2D[] playerDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);

                Debug.Log(playerDamage.Length);
                for (int i = 0; i < playerDamage.Length; i++) {
                    if (playerDamage[i].gameObject.CompareTag("Player")) {
                        playerDamage[i].GetComponentInParent<PlayerMovement>().Knockback(transform.position, 500);
                        playerDamage[i].GetComponentInParent<PlayerMovement>().TakeDamage(damage);
                    }
                }

                //Debug.Log("Attacking");
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

        if (projectileBuffer >= 0) {
            projectileBuffer -= Time.deltaTime;
        }

        // Projectile detection
        if (phase == 1) {
            if (Vector2.Distance(player.transform.position, transform.position) <= projectileRange && projectileBuffer <= 0){
                // Shoot projectile
                Shoot();
                projectileBuffer = 10f;
                Debug.Log("Is Shooting");
            }
        }

        if (phase == 2) {
            aipath.maxSpeed = 3;

            if (Vector2.Distance(player.transform.position, transform.position) <= projectileRange && Vector2.Distance(player.transform.position, transform.position) >= projectileSafety && projectileBuffer <= 0)
            {
                // Shoot projectile 
                Shoot();
                projectileBuffer = 10f;
            }
        }

        if (health <= 0) {
            health = 0;
        }


    }

    public void LookAtPlayer() {

        // Flip sprite
        if (transform.position.y > player.position.y && isFlipped && !isAttacking)
        {
            isFlipped = false;
        }
        else if (transform.position.y < player.position.y && !isFlipped && !isAttacking)
        {
            isFlipped = true;
        }

        // Attack tracking
        if (!isAttacking)
        {
            Vector3 difference = player.transform.position - transform.position;

            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            pivot.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }

    }

    // Shoot projectile
    void Shoot() {
        GameObject bullet = Instantiate(bossBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
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
                if (!isAttacking && attackBuffer <= 0 && !other.gameObject.GetComponent<PlayerMovement>().isDashing)
                {
                    isAttacking = true;
                }
            }
        }
    }
}
