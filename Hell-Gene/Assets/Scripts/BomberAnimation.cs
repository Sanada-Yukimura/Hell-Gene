using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAnimation : MonoBehaviour
{
    public Animator animator;
    Enemy bomber;



   void Start() {
        bomber = GetComponentInParent<Enemy>();
     
    }
    void Update()
    {      
        bool isExplode = bomber.isExploding;


       animator.SetBool("isExploding", isExplode);
        //Debug.Log(isExploding);
    }
}