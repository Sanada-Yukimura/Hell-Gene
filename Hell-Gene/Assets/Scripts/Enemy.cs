using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int maxHealth;
    public int health;


    public Pathfinding.AIPath aipath;

    public bool hitStun;
    private float hitStunTimer;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        healthBar.fillAmount = (float)health / maxHealth;
    }
}
