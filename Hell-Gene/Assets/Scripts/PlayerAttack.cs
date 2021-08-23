using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool isAttacking = false;

    
    private float attackCooldown;
    public float startAttackCooldown;

    private float gunCooldown;
    public float startGunCooldown;

    public int meleeType = 0;
    public int rangeType = 0;

    public int damage;

    Transform player;

    public LayerMask whatIsEnemy;

    public Transform attackPos;
    public float attackRange;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public int bulletForce;

    private Vector3 mouseDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();

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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 mouseDir = (mousePos - transform.position).normalized;

        Vector3 attackPos = transform.position + mouseDir * 3;
    }

    void MeleeAttack() {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
        attackCooldown = startAttackCooldown;
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
