using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth;
    public float health;
    GameObject thePlayer;
    
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        PlayerMovement player = thePlayer.GetComponent<PlayerMovement>();
        maxHealth = player.health;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement player = thePlayer.GetComponent<PlayerMovement>();
        health = player.health;
        healthBar.fillAmount = (float)health / maxHealth;
    }
}
