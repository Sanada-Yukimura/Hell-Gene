using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool isAttacking = false;

    private float attackCooldown;
    public float startAttackCooldown;

    public int meleeType = 0;
    public int rangeType = 0;

    Transform player;

    public LayerMask whatIsEnemy;

    public Transform attackPos;
    public float attackRange;

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

        if (attackCooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MeleeAttack();
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

    }

    void GetAttackDirection() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 mouseDir = (mousePos - transform.position).normalized;

        Vector3 attackPos = transform.position + mouseDir * 3;

        Debug.Log(mouseDir);
    }

    void MeleeAttack() { 
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
