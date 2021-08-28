using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimationTest : MonoBehaviour
{
    public Animator swordSlash;
    //PlayerAttack playerAttack;
    GameObject playerAtk;
    Vector3 offSetPos;
        public GameObject player;
        public Transform playerPos;
    // Update is called once per frame

    void Start() {
        //playerAttack = GetComponentInParent<PlayerAttack>();
        playerAtk = GameObject.FindGameObjectWithTag("Player");
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //swordSlash.Rebind();
    }

    void Update()
    {        
        
        bool isAtk = playerAtk.GetComponent<PlayerAttack>().isAttacking;
       swordSlash.SetBool("IsAttacking", isAtk);
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        offSetPos = new Vector3(playerPos.position.x,playerPos.position.y,playerPos.position.z);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        transform.position = offSetPos;
    }
}
