using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    //facing (0=nothing,1=Up,2=Right,3=Left,4=Down)
	public int facing = 0;

    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
  
    public bool isInvincible;
    public float invincibleCountdown;

    public bool isDashing;
    private float dashTimer;

    public int health = 50;

    public bool canMove = true;
    private float moveCountdown = 0.5f;

    private Vector2 moveDirection;

    public Vector2 lookDir;
    float angle;
    public Vector2 mousePos;
	public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInvincible = false;
        canMove = true;
    }

    private void checkFacing()
	{

		//Uses camera to create ingame pixel units for distance
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


		animator.SetFloat("Speed", moveSpeed);	
		animator.SetFloat("Facing", facing);		
		
		//Vector for mouse position - player position
		lookDir = mousePos-rb.position;

		//In reference t0 the mouse pos;

		//Angle after math
		angle = Mathf.Atan2(lookDir.y, lookDir.x);
		//Conversion from radians to degrees with the fixing of the offset of 45
		angle *= Mathf.Rad2Deg;
		if (angle < 0) {
			angle += 360;
		}
		Debug.Log(angle);

		if (angle > 45f && angle <= 135f) {
			facing = 1;
		}

		else if (angle > 135f && angle <= 225f) {
			facing = 3;
		}

		else if (angle > 225f && angle <= 315f) {
			facing = 4;
		}
		else {
			facing = 2;
		}
		
		
		
		
		//Displaying to to two digits

//		if(angle>-225f && angle<-135f)
//		{
//		 facing = 4;	//Char is facing down		
//		}
//		else if(angle>-45f && angle<45f)
//		{
//		 facing = 1;	//Char is facing up		
//		}
//		else if((angle>45.01f && angle<90f) || (angle<-225f && angle>-270f))
//		{
//		 facing = 3;	//Char is facing left	
//		}
//		else 
//		{
//		 facing = 2;	//Char is facing right	
//		}		



	}



    // Update is called once per frame
    void Update()
    {
        Inputs();
        checkFacing();
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
            dashTimer = 0.05f;
        }

        if (isDashing) {
            isInvincible = true;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0) {
                isDashing = false;
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
		animator.SetFloat("Horizontal", moveX);
		animator.SetFloat("Vertical", moveY);
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
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

    private void TakeDamage(int damage)
    {
        health -= damage;

    }

    void Dash() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed * 10, moveDirection.y * moveSpeed * 10);
    }

}
