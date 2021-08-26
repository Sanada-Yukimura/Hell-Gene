using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;

    public bool isInvincible;
    public float invincibleCountdown;

    public bool isDashing;
    private float dashTimer;

    public int health = 50;

    public bool canMove = true;
    private float moveCountdown = 0.5f;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInvincible = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        if (invincibleCountdown >= 0) {
            invincibleCountdown -= Time.deltaTime;
        }
        if (invincibleCountdown <= 0) {
            isInvincible = false;
        }

        if (canMove) {
            moveCountdown = 0.05f;
        }

        if (!canMove) {
            moveCountdown -= Time.deltaTime;
        }

        if (moveCountdown <= 0) {
            canMove = true;
        }

        if (!isDashing) {
            dashTimer = 0.1f;
        }

        if (isDashing) {
            isInvincible = true;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0) {
                isDashing = false;
                invincibleCountdown = -0.1f;
                isInvincible = false;
                canMove = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }

        if (isDashing) {
            Dash();
        }

    }

    void Inputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX != 0 || moveY !=0))
        {
            isDashing = true;
        }
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemycollider"))
        {
            Vector3 enemy = collision.gameObject.GetComponentInParent<Enemy>().transform.position;
            Vector3 knockback = (transform.position - enemy).normalized;

            if (!isInvincible) {
                rb.AddForce(knockback * 30, ForceMode2D.Impulse);

                collision.gameObject.GetComponentInParent<Enemy>().hitStun = true;

                int enemyDamage = collision.gameObject.GetComponentInParent<Enemy>().damage;
                TakeDamage(enemyDamage);

                isInvincible = true;
                invincibleCountdown = 1.0f;

                canMove = false;
            }
        }
        if (collision.CompareTag("BossCollider"))
        {
            Vector3 enemy = collision.gameObject.GetComponentInParent<Boss>().transform.position;
            Vector3 knockback = (transform.position - enemy).normalized;

            if (!isInvincible)
            {
                rb.AddForce(knockback * 80, ForceMode2D.Impulse);

                int enemyDamage = collision.gameObject.GetComponentInParent<Boss>().damage;
                TakeDamage(enemyDamage);

                isInvincible = true;
                invincibleCountdown = 1.0f;

                canMove = false;
            }
        }
    }

    public void Knockback(Vector3 position, int force)
    {
        Vector3 knockbackDir = (transform.position - position).normalized;

        rb.AddForce(knockbackDir * force);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            isInvincible = true;
            invincibleCountdown = 1.0f;
        }

    }

    void Dash() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed * 10, moveDirection.y * moveSpeed * 10);
    }

}
