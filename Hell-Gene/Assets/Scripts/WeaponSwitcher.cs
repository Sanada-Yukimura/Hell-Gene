using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
	private GameObject player;

	public Sprite basicGun, bubbleGun, sharkGun, teethiGun, laserGun, decomposter;
	public Sprite basicMelee, scythe, fish, busterBlade, nipponSteel, onlyFan;

	// if weapon type is ranged, int = 1, melee, int = 2
	public int weaponType; 
	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindWithTag("Player");

	}

	// Update is called once per frame
	void Update()
	{
		if (weaponType == 1) {
			int weapon = player.GetComponent<PlayerAttack>().rangeType;
//			Debug.Log("Ranged Weapon is:"+weapon);
			if (weapon == 0) {
				GetComponent<Image>().sprite = basicGun;
			}
			else if (weapon == 1) {
				GetComponent<Image>().sprite = teethiGun;
			}
			else if (weapon == 2) {
				GetComponent<Image>().sprite = sharkGun;
			}
			else if (weapon == 3) {
				GetComponent<Image>().sprite = decomposter;
			}
			else if (weapon == 4) {
				GetComponent<Image>().sprite = laserGun;
			}
			else if (weapon == 5) {
				GetComponent<Image>().sprite = bubbleGun;
			}
		}
		else if (weaponType == 2) {
			int weapon = player.GetComponent<PlayerAttack>().meleeType;
			if (weapon == 0) {
				GetComponent<Image>().sprite = basicMelee;
			}
			else if (weapon == 1) {
				GetComponent<Image>().sprite = scythe;
			}
			else if (weapon == 2) {
				GetComponent<Image>().sprite = fish;
			}
			else if (weapon == 3) {
				GetComponent<Image>().sprite = busterBlade;
			}
			else if (weapon == 4) {
				GetComponent<Image>().sprite = nipponSteel;
			}
			else if (weapon == 5) {
				GetComponent<Image>().sprite = onlyFan;
			}
		}
	}
}

