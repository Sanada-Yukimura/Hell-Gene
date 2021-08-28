using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBarController : MonoBehaviour {
	public int weaponCategory;
	public Image durabilityBar;

	private GameObject player;
    // Start is called before the first frame update
    void Start() {
	    player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
	    if (weaponCategory == 1) {
		    int weapon = player.GetComponent<PlayerAttack>().rangeType;
		    //Debug.Log("Ranged Weapon is:"+weapon);
		    if (weapon == 0) {
			    durabilityBar.fillAmount = 1;
		    }
		    else if (weapon == 1) {
			    Debug.Log(player.GetComponent<PlayerAttack>().rangedDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().rangedDurability / 100f;
		    }
		    else if (weapon == 2) {
			    Debug.Log(player.GetComponent<PlayerAttack>().rangedDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().rangedDurability / 10f;
		    }
		    else if (weapon == 3) {
			    Debug.Log(player.GetComponent<PlayerAttack>().rangedDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().rangedDurability / 20f;
		    }
		    else if (weapon == 4) {
			    Debug.Log(player.GetComponent<PlayerAttack>().rangedDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().rangedDurability / 5f;
		    }
		    else if (weapon == 5) {
			    Debug.Log(player.GetComponent<PlayerAttack>().rangedDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().rangedDurability / 10f;
		    }
	    }
	    else if (weaponCategory == 2) {
		    int weapon = player.GetComponent<PlayerAttack>().meleeType;
		    if (weapon == 0) {
			    durabilityBar.fillAmount = 1;
		    }
		    else if (weapon == 1) {
			    Debug.Log(player.GetComponent<PlayerAttack>().meleeDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().meleeDurability / 30f;
		    }
		    else if (weapon == 2) {
			    Debug.Log(player.GetComponent<PlayerAttack>().meleeDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().meleeDurability / 60f;
		    }
		    else if (weapon == 3) {
			    Debug.Log(player.GetComponent<PlayerAttack>().meleeDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().meleeDurability / 5f;
		    }
		    else if (weapon == 4) {
			    Debug.Log(player.GetComponent<PlayerAttack>().meleeDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().meleeDurability / 25f;
		    }
		    else if (weapon == 5) {
			    Debug.Log(player.GetComponent<PlayerAttack>().meleeDurability);
			    durabilityBar.fillAmount = player.GetComponent<PlayerAttack>().meleeDurability / 10f;
		    }
	    }
    }
}
