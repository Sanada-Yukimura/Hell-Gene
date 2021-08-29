using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void playGame(){
	    if (GameObject.FindGameObjectWithTag("NormalMusic")) {
		    Destroy(GameObject.FindGameObjectWithTag("NormalMusic"));
	    } 
	    
	    PlayerPrefs.SetInt("CurrentChaos", 0);
	    PlayerPrefs.SetInt("NextSceneNumber", 1);
	    PlayerPrefs.SetInt("currentMeleeType", 0);
	    PlayerPrefs.SetInt("currentRangedType", 0);
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }

    public void quitGame(){
        Application.Quit();
    }
}
