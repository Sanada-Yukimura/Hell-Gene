using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image chaosBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
	    var currentChaos = PlayerPrefs.GetInt("CurrentChaos", 0);

	    if (currentChaos == 0) {
		    chaosBar.fillAmount = 0f;
	    }
	    else if (currentChaos == 1) {
		    chaosBar.fillAmount = 0.234f;
	    }
	    else if (currentChaos == 2) {
		    chaosBar.fillAmount = 0.491f;
	    }
	    else if (currentChaos == 3) {
		    chaosBar.fillAmount = 0.762f;
	    }
	    else if (currentChaos >= 4) {
		    chaosBar.fillAmount = 1;
	    }
    }
}
