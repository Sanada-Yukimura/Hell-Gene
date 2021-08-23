using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;

    public bool isInvincible;
    public float invincibleCountdown;

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

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Inputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 enemy = collision.gameObject.GetComponent<Enemy>().transform.position;
            Vector3 knockback = (transform.position - enemy).normalized;

            if (!isInvincible) {
                rb.AddForce(knockback * 30, ForceMode2D.Impulse);

                collision.gameObject.GetComponent<Enemy>().hitStun = true;

                int enemyDamage = collision.gameObject.GetComponent<Enemy>().damage;
                TakeDamage(enemyDamage);

                isInvincible = true;
                invincibleCountdown = 1.0f;

                canMove = false;
            }
        }

    }

    private void TakeDamage(int damage)
    {
        health -= damage;

    }

 

}
