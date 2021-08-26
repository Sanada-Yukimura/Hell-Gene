using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChaosCardSelect : MonoBehaviour {
	public Button card;
    // Start is called before the first frame update
    void Start()
    {
	    card.onClick.AddListener(OnClicked);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked() {
	    if (card.name == "RetainChaos") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 0));
		    Debug.Log("Retain Chaos");
	    }

	    else if (card.name == "RaiseChaos") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 0)+1);
		    Debug.Log("Raise Chaos");
	    }
    }
}
