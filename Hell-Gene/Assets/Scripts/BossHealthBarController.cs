using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour {
	public Image bossHealthBar;

	public GameObject boss;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
	    bossHealthBar.fillAmount = boss.GetComponent<Boss>().health / (float) boss.GetComponent<Boss>().maxHealth;
    }
}
