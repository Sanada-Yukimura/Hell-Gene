using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool isAttacking = false;
    private float isAttackingCooldown;
    
    private float attackCooldown;
    public float startAttackCooldown;

    private float gunCooldown;
    public float startGunCooldown;

    public int meleeType = 0;
    public int rangeType = 0;

    public int damage;
    public int knockbackForce; // Needs to be a big number in the 100s at least
    public int lungeForce; // Force to move forward on final hit

    public int maxCombo;
    private int combo = 0;
    public float comboCooldown;
    private float comboCountdown;

    Transform player;

    public LayerMask whatIsEnemy;

    public Transform attackPos;
    public float attackRange;

    public Transform firePoint;
    public GameObject bulletPrefab;

    PlayerMovement playerMov;

    Rigidbody2D rb;

    public int bulletForce; // Speed of the bullet

    private Vector3 mouseDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        playerMov = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();

        if (isAttacking) {
            isAttackingCooldown -= Time.deltaTime;
            if (isAttackingCooldown <= 0) {
                isAttacking = false;
            }
        }

        if (!isAttacking) {
            isAttackingCooldown = comboCooldown;
        }



        if (comboCountdown >= 0) {
            comboCountdown -= Time.deltaTime;
        }

        if (comboCountdown <= 0) {
            combo = 0;
        }

        // Melee attacks
        if (attackCooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                MeleeAttack();
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

        // Gun attacks
        if (gunCooldown <= 0)
        {
            if (Input.GetMouseButtonDown(1)) // Right click
            {
                RangedAttack();
            }
        }
        else {
            gunCooldown -= Time.deltaTime;
        }


        // Changing melee weapon stats based on which is equipped
        switch(meleeType){
            case 0:
                startAttackCooldown = 0.08f;
                attackRange = 0.8f;
                damage = 10;
                maxCombo = 3;
                comboCooldown = 0.4f;
                knockbackForce = 1000;
                lungeForce = 500;
                break;
        }

        // Changing ranged weapon stats based on which is equipped
        switch (rangeType) {
            case 0:
                startGunCooldown = 0.5f;


                break;
        }

    }

    void GetAttackDirection() {
        // I feel like this is useless now but I'm not sure
        // because other parts still use it and I'm too afraid to delete it
        // so please leave it in.

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 mouseDir = mousePos.normalized;


        Vector3 attackPos = transform.position + mouseDir * 3;
    }

    void MeleeAttack() {
        combo += 1;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        isAttacking = true;

        // Regular combos
        if (combo < maxCombo)
        {
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
                enemiesToDamage[i].GetComponentInParent<Enemy>().Knockback(transform.position, knockbackForce);
            }
            attackCooldown = startAttackCooldown;
            comboCountdown = comboCooldown;
        
        // Final combo
        } else if (combo == maxCombo) {
            
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage * 2);
                enemiesToDamage[i].GetComponentInParent<Enemy>().Knockback(transform.position, (knockbackForce * 5));
            }
            attackCooldown = startAttackCooldown * 4;
            combo = 0;
            comboCountdown = 0;

            Vector3 enemyTarget = enemiesToDamage[0].GetComponentInParent<Enemy>().transform.position;
            Vector3 lungeDir = (enemyTarget - transform.position).normalized;
            rb.AddForce(lungeDir * lungeForce);

            playerMov.isInvincible = true;
            playerMov.invincibleCountdown = 0.2f;
        }


    }

    void RangedAttack() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos() // Debug hitboxes
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

        //Gizmos.DrawWireSphere(firePoint.position, 0.5f);
    }

}
