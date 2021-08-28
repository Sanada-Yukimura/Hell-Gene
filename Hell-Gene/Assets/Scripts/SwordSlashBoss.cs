using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashBoss : MonoBehaviour
{

    public Animator swordSlash;
    //PlayerAttack playerAttack;
    GameObject bossAtk;
    Vector3 offSetPos;
    public Transform playerPos;
        //public GameObject player;
    public Transform bossPos;
    // Update is called once per frame

    void Start() {
        //playerAttack = GetComponentInParent<PlayerAttack>();
        bossAtk = GameObject.FindGameObjectWithTag("Boss");
        bossPos = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //swordSlash.Rebind();
    }

    void Update()
    {      
        bool isAtk = bossAtk.GetComponent<Boss>().attackFrames; 


       swordSlash.SetBool("IsAttacking", isAtk);
        Debug.Log(isAtk);
    }

    private void FixedUpdate()
    {
        Vector3 difference = playerPos.position - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        offSetPos = new Vector3(bossPos.position.x,bossPos.position.y,bossPos.position.z);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        transform.position = offSetPos;
    }
}


