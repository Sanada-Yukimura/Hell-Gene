using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosHandler : MonoBehaviour
{
    //temp chaos handler file for debugging purporses
    public int chaosLevel;

    // Start is called before the first frame update
    void Start() {
	    chaosLevel = PlayerPrefs.GetInt("CurrentChaos", 0);
        //Debug.Log("Current Chaos is: " + chaosLevel);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
