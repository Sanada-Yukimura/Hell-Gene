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

    private bool isMouseDown; //checks if the mouse is down

    Vector3 enemyTarget;

    Transform player;

    public LayerMask whatIsEnemy;

    public Transform attackPos;
    public float attackRange;

    public Transform firePoint;
    public GameObject[] bulletPrefabs;

    PlayerMovement playerMov;

    Rigidbody2D rb;

    private int bulletForce = 20; // Speed of the bullet
    private int ammo;

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
            if (Input.GetMouseButtonDown(0) && combo < maxCombo) // Left click
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
            switch (rangeType) //What do on right click
            {
                case 0: //basic
                    if (Input.GetMouseButtonDown(1))
                    {
                        gunCooldown = 0.4f;
                        RangedAttack(); //single shots
                    }
                    break;
                case 1: //minigun
                    if (Input.GetMouseButton(1))
                    {
                        gunCooldown = 0.1f;
                        RangedAttack(); //held shot
                    }
                    break;
                case 2: //grenade launcher
                    if (Input.GetMouseButtonDown(1))
                    {
                        gunCooldown = 1.5f;
                        RangedAttack(); //single shots
                    }
                    break;
                case 3: //decomposer
                    if (Input.GetMouseButtonDown(1))
                    {
                        gunCooldown = 0.8f;
                        RangedAttack(); //single shots
                    }
                    break;
                case 4: //laser
                    if (Input.GetMouseButtonDown(1))
                    {
                        gunCooldown = 2f;
                        Invoke("RangedAttack", 1f); //single shots
                    }
                    break;
                case 5: //bubble
                    if (Input.GetMouseButtonDown(1))
                    {
                        gunCooldown = 1f;
                        RangedAttack(); //single shots
                    }
                    break;
                default:
                    if (Input.GetMouseButtonDown(1)) RangedAttack();
                    break;

            }
        }
        else {
            gunCooldown -= Time.deltaTime;
        }


        // Changing melee weapon stats based on which is equipped
        switch(meleeType){
            case 0: //default
                startAttackCooldown = 0.08f;
                attackRange = 0.8f;
                damage = 10;
                maxCombo = 3;
                comboCooldown = 0.4f;
                knockbackForce = 1000;
                lungeForce = 500;
                break;
            case 1: //scythe
                startAttackCooldown = 0.08f;
                attackRange = 1.2f;
                damage = 20;
                maxCombo = 3;
                comboCooldown = 0.4f;
                knockbackForce = 1200;
                lungeForce = 500;
                break;
            case 2: //fish
                startAttackCooldown = 0.04f;
                attackRange = 0.6f;
                damage = 8;
                maxCombo = 3;
                comboCooldown = 0.2f;
                knockbackForce = 500;
                lungeForce = 500;
                break;
            case 3: //buster
                startAttackCooldown = 1.0f;
                attackRange = 1.0f;
                damage = 50;
                maxCombo = 3;
                comboCooldown = 0.8f;
                knockbackForce = 2000;
                lungeForce = 500;
                break;
            case 4: //katana
                startAttackCooldown = 0.1f;
                attackRange = 0.9f;
                damage = 15;
                maxCombo = 3;
                comboCooldown = 0.5f;
                knockbackForce = 1000;
                lungeForce = 800;
                break;
            case 5: //fan
                startAttackCooldown = 0.1f;
                attackRange = 1.5f;
                damage = 10;
                maxCombo = 3;
                comboCooldown = 0.5f;
                knockbackForce = 2500;
                lungeForce = 500;
                break;
            default:
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
            case 0: //default
                startGunCooldown = 0.5f;
                bulletForce = 20;
                break;
            case 1: 
                startGunCooldown = 0.5f;
                bulletForce = 25;
                break;
            case 2:
                startGunCooldown = 0.5f;
                bulletForce = 10;
                break;
            case 3: //default
                startGunCooldown = 0.5f;
                bulletForce = 20;
                break;
            case 4: //default
                startGunCooldown = 0.5f;
                bulletForce = 0;
                break;
            case 5: //default
                startGunCooldown = 0.5f;
                bulletForce = 5;
                break;
            default:
                startGunCooldown = 0.5f;
                bulletForce = 20;
                break;
        }

    }

    void GetAttackDirection() {
        // I feel like this is useless now but I'm not sure
        // because other parts still use it and I'm too afraid to delete it
        // so please leave it in.

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 mouseDir = mousePos.normalized;


        //Vector3 attackPos = transform.position + mouseDir * 3;
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
                if (enemiesToDamage[i].gameObject.CompareTag("Enemycollider"))
                {
                    enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
                    enemiesToDamage[i].GetComponentInParent<Enemy>().Knockback(transform.position, knockbackForce);
                    Debug.Log("Attacking Enemy");
                }
                else if (enemiesToDamage[i].gameObject.CompareTag("BossCollider")) {
                    enemiesToDamage[i].GetComponentInParent<Boss>().TakeDamage(damage);
                    enemiesToDamage[i].GetComponentInParent<Boss>().Knockback(transform.position, knockbackForce);
                    Debug.Log("Attacking Boss");
                }
            }
            attackCooldown = startAttackCooldown;
            comboCountdown = comboCooldown;
        
        // Final combo
        } else if (combo == maxCombo) {

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].gameObject.CompareTag("Enemycollider"))
                {
                    enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage * 2);
                    enemiesToDamage[i].GetComponentInParent<Enemy>().Knockback(transform.position, (knockbackForce * 5));
                    Debug.Log("Attacking Enemy");
                }
                else if (enemiesToDamage[i].gameObject.CompareTag("BossCollider")) {
                    enemiesToDamage[i].GetComponentInParent<Boss>().TakeDamage(damage * 2);
                    enemiesToDamage[i].GetComponentInParent<Boss>().Knockback(transform.position, (knockbackForce * 3));
                    Debug.Log("Attacking Boss");
                }
            }
            attackCooldown = startAttackCooldown * 4;
            combo = 0;
            comboCountdown = 0;

            if (enemiesToDamage[0].gameObject.CompareTag("Enemy"))
            {
                Vector3 enemyTarget = enemiesToDamage[0].GetComponentInParent<Enemy>().transform.position;
            }
            else if (enemiesToDamage[0].gameObject.CompareTag("Boss")) {
                Vector3 enemyTarget = enemiesToDamage[0].GetComponentInParent<Boss>().transform.position;
            }
            Vector3 lungeDir = (enemyTarget - transform.position).normalized;
            rb.AddForce(lungeDir * lungeForce);

            playerMov.isInvincible = true;
            playerMov.invincibleCountdown = 0.2f;
        }


    }

    void RangedAttack() {
        //load bullet
        GameObject bulletPrefab = bulletPrefabs[0];
        if(rangeType != 0) bulletPrefab = bulletPrefabs[rangeType];
        //fire bullet
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
