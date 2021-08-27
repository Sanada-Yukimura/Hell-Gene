using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChaosCardSelect : MonoBehaviour {
	public Button card;

	private int nextSceneNumber;

	private bool loadSceneNow = false;
    // Start is called before the first frame update
    void Start()
    {
	    card.onClick.AddListener(OnClicked);
	    nextSceneNumber = PlayerPrefs.GetInt("nextSceneInt", 1)+1;
//	    StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked() {
	    if (card.name == "RetainChaos") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 0));
		    loadSceneNow = true;
		    Debug.Log("Retain Chaos");
		    if (nextSceneNumber < 5) {
			    SceneManager.LoadScene("Scenes/LevelGenTest");
		    }
		    else if (nextSceneNumber == 5) {
			    SceneManager.LoadScene("Scenes/LevelGenTest"); // Set it to Boss Room
		    }
		    
		    
	    }

	    else if (card.name == "RaiseChaos") {
		    PlayerPrefs.SetInt("currChaos", PlayerPrefs.GetInt("currChaos", 0)+1);
		    loadSceneNow = true;
		    Debug.Log("Raise Chaos");
		    if (nextSceneNumber < 5) {
			    SceneManager.LoadScene("Scenes/LevelGenTest");
		    }
		    else if (nextSceneNumber == 5) {
			    SceneManager.LoadScene("Scenes/LevelGenTest"); // Set it to Boss Room
		    }
	    }
    }


//    IEnumerator LoadScene() {
//	    AsyncOperation asyncLoad;
//	    if (nextSceneNumber < 5) {
//		    asyncLoad = SceneManager.LoadSceneAsync("Scenes/LevelGenTest"); // Load Regular Scene
//	    }
//	    else {
//		    asyncLoad = SceneManager.LoadSceneAsync("Scenes/LevelGenTest"); // Load Boss Scene
//	    }
//	    
//	    asyncLoad.allowSceneActivation = false;
//	    while (!asyncLoad.isDone) {
//		    Debug.Log("not done");
//		    yield return null;
//	    }
//
//	    if (asyncLoad.isDone && loadSceneNow) {
//		    asyncLoad.allowSceneActivation = true;
//	    }
//	    
//    }
}
