using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChaosCard : MonoBehaviour {
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
	    if (card.name == "CardLeft") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 1));
//		    SceneManager.LoadScene();
	    }

	    else if (card.name == "CardRight") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 1)+1); 
//		    SceneManager.LoadScene();
	    }
    }

    
}
