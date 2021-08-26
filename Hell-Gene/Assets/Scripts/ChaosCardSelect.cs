using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
		    Debug.Log("RetainChaos");
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 1));
		    AnimateCard();
	    }
	    else if (card.name == "RaiseChaos") {
		    Debug.Log("RaiseChaos");
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 1)+1);
	    }
    }
    

    void AnimateCard() {
	    card.GetComponent<Animator>().SetBool("AnimateCard", true);
    }

    void StopAnimate() {
	    card.GetComponent<Animator>().SetBool("AnimateCard", false);
    }
    
}
