using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //facing (0=nothing,1=Up,2=Right,3=Left,4=Down)
    public int facing = 0;
    public Animator animator;
    //public Animator swordSlash;
    public Vector2 lookDir;
    float angle;
    public Vector2 mousePos;
    public Camera cam;

    public float moveSpeed;
    public Rigidbody2D rb;

    public bool isInvincible;
    public float invincibleCountdown;

    public bool isDashing;
    private float dashTimer;

    public int health = 100;

    public bool canMove = true;
    private float moveCountdown = 0.5f;

    private Vector2 moveDirection;

    PlayerAttack playerAttack;
    public GameObject gameOver;

    public AudioSource hitAudio;

    //particles
    public GameObject dashParticles;
    private GameObject dash;
    private bool dashPlayedOnce = false;

    // Start is called before the first frame update
    void Start() {
	    
        rb = GetComponent<Rigidbody2D>();
        isInvincible = false;
        canMove = true;

        playerAttack = GetComponent<PlayerAttack>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Inputs();
        checkFacing();
        if(health<=0){
	        
            canMove = false;
            gameOver.SetActive(true);
        }

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

        if (moveCountdown <= 0 && health> 0) {
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
                dashPlayedOnce = false;
                invincibleCountdown = -0.1f;
                isInvincible = false;
                canMove = true;
            }
        }

        animator.SetBool("IsAttacking", playerAttack.isAttacking);
        //swordSlash.SetBool("IsAttacking", playerAttack.isAttacking);
        animator.SetFloat("Combo", (float)playerAttack.combo);
        //Debug.Log((float)playerAttack.combo);

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX != 0 || moveY !=0))
        {
            isDashing = true;

            Quaternion particleRotation = Quaternion.Euler(0f, 0f, 0f);

            if (moveDirection.x == 0 && moveDirection.y == -1) particleRotation = Quaternion.Euler(0f, 0f, 0f);
            if (moveDirection.x == 1 && moveDirection.y == -1) particleRotation = Quaternion.Euler(0f, 0f, 45f);
            if (moveDirection.x == 1 && moveDirection.y == 0) particleRotation = Quaternion.Euler(0f, 0f, 90f);
            if (moveDirection.x == 1 && moveDirection.y == 1) particleRotation = Quaternion.Euler(0f, 0f, 135f);
            if (moveDirection.x == 0 && moveDirection.y == 1) particleRotation = Quaternion.Euler(0f, 0f, 180f);
            if (moveDirection.x == -1 && moveDirection.y == 1) particleRotation = Quaternion.Euler(0f, 0f, 225f);
            if (moveDirection.x == -1 && moveDirection.y == 0) particleRotation = Quaternion.Euler(0f, 0f, 270f);
            if (moveDirection.x == -1 && moveDirection.y == -1) particleRotation = Quaternion.Euler(0f, 0f, 315f);

            dash = Instantiate(dashParticles, transform.position, particleRotation); //i am speed (particles)
            if (!dashPlayedOnce)
            {
                dash.GetComponent<ParticleSystem>().Play();
                Destroy(dash, 0.5f);
                dashPlayedOnce = true;
            }
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

                //particle
                Vector3 relativePos = transform.position - enemy;
                Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                GameObject hitParticle = Instantiate(hitParticleContainer, transform.position, particleRotation);
                hitParticle.GetComponent<ParticleSystem>().Play();
                Destroy(hitParticle, 0.5f); //delete particles

                collision.gameObject.GetComponentInParent<Enemy>().hitStun = true;

                int enemyDamage = collision.gameObject.GetComponentInParent<Enemy>().damage;
                TakeDamage(enemyDamage/2);

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

                //particle
                Vector3 relativePos = transform.position - enemy;
                Quaternion particleRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                GameObject hitParticleContainer = GameObject.FindGameObjectWithTag("HitParticle");
                GameObject hitParticle = Instantiate(hitParticleContainer, transform.position, particleRotation);
                hitParticle.GetComponent<ParticleSystem>().Play();
                Destroy(hitParticle, 0.5f); //delete particles

                int enemyDamage = collision.gameObject.GetComponentInParent<Boss>().damage;
                TakeDamage(enemyDamage/2);

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
        Vector2 movement = new Vector2(moveDirection.x * moveSpeed * 10, moveDirection.y * moveSpeed * 10);
        rb.velocity = movement;
    }

    private void checkFacing()
    {

        //Uses camera to create ingame pixel units for distance
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        animator.SetFloat("Speed", moveSpeed);
        animator.SetFloat("Facing", facing);

        //Vector for mouse position - player position
        lookDir = mousePos - rb.position;

        //In reference t0 the mouse pos;

        //Angle after math
        angle = Mathf.Atan2(lookDir.y, lookDir.x);
        //Conversion from radians to degrees with the fixing of the offset of 45
        angle *= Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        //Debug.Log(angle);

        if (angle > 45f && angle <= 135f)
        {
            facing = 1;
        }

        else if (angle > 135f && angle <= 225f)
        {
            facing = 3;
        }

        else if (angle > 225f && angle <= 315f)
        {
            facing = 4;
        }
        else
        {
            facing = 2;
        }

    }

}
