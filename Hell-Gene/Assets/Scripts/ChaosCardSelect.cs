using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChaosCardSelect : MonoBehaviour {
	public Button card;

	public int nextSceneNumber;

	private bool loadSceneNow = false;

	public int chaosLevel;
    // Start is called before the first frame update
    void Start()
    {
	    card.onClick.AddListener(OnClicked);
	    nextSceneNumber = PlayerPrefs.GetInt("NextSceneNumber", 1)+1;
	    
	    chaosLevel = PlayerPrefs.GetInt("CurrentChaos", 0);
	    Debug.Log("Current Chaos is: "+chaosLevel);
        Debug.Log("NextSceneInt is: " + nextSceneNumber);
        //	    StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked() {
	    if (card.name == "RetainChaos") {
		    PlayerPrefs.SetInt("CurrentChaos", chaosLevel);
		    
		    loadSceneNow = true;
		    
		    if (nextSceneNumber < 5) {
			    PlayerPrefs.SetInt("NextSceneNumber", nextSceneNumber);
			    SceneManager.LoadScene("Scenes/LevelGenTest");
		    }
		    else if (nextSceneNumber >= 5) {
			    SceneManager.LoadScene("Scenes/BossTest"); // Set it to Boss Room
		    }
	    }

	    else if (card.name == "RaiseChaos") {
		    PlayerPrefs.SetInt("CurrentChaos", chaosLevel+1);
		    loadSceneNow = true;
		    
		    if (nextSceneNumber < 5) {
			    PlayerPrefs.SetInt("NextSceneNumber", nextSceneNumber);
			    SceneManager.LoadScene("Scenes/LevelGenTest");
		    }
		    else if (nextSceneNumber >= 5) {
			    SceneManager.LoadScene("Scenes/BossTest"); // Set it to Boss Room
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
