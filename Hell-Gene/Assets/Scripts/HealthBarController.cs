using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
	public GameObject player;
	public Image healthBar;
	private float playerMaxHealth;

	private float currentHealth;
    // Start is called before the first frame update
    void Start() {
	    playerMaxHealth = 100f;

    }

    // Update is called once per frame
    void Update() {

	    currentHealth = player.GetComponent<PlayerMovement>().health / playerMaxHealth;
	    if (currentHealth < 0) {
		    currentHealth = 0;
	    }
	    healthBar.fillAmount = currentHealth;
	    
	    
    }
}
