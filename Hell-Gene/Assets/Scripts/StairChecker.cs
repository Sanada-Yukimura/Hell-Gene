using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairChecker : MonoBehaviour
{

    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnStaircase(GameObject staircase)
    {
        if (!spawned)
        {
            Instantiate(staircase, transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Player")) {

            //transfer weapon properties
            PlayerPrefs.SetInt("currentMeleeType", other.gameObject.GetComponent<PlayerAttack>().meleeType);
            PlayerPrefs.SetInt("currentMeleeDurability", other.gameObject.GetComponent<PlayerAttack>().meleeDurability);

            PlayerPrefs.SetInt("currentRangedType", other.gameObject.GetComponent<PlayerAttack>().rangeType);
            PlayerPrefs.SetInt("currentRangedDurability", other.gameObject.GetComponent<PlayerAttack>().rangedDurability);

            SceneManager.LoadScene("Scenes/ChaosCardTest");
	    }
    }
}
