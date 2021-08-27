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

    public float attackMod;
    // Start is called before the first frame update
    void Start()
    {
	    card.onClick.AddListener(OnClicked);
	    nextSceneNumber = PlayerPrefs.GetInt("CurrentChaos", 0)+1;
	    chaosLevel = PlayerPrefs.GetInt("CurrentChaos", 0);
        attackMod = PlayerPrefs.GetFloat("AttackMod", 1);
        Debug.Log("Current Chaos is: "+chaosLevel);
        Debug.Log("NextSceneInt is: " + chaosLevel);
        //	    StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked() {
	    if (card.name == "RetainChaos") {
		    PlayerPrefs.SetInt("CurrentChaos", chaosLevel);
            PlayerPrefs.SetFloat("AttackMod", attackMod + 0.01f);
            loadSceneNow = true;
		    Debug.Log("Retain Chaos");
		    if (nextSceneNumber < 5) {
			    SceneManager.LoadScene("Scenes/LevelGenTest");
		    }
		    else if (nextSceneNumber >= 5) {
			    SceneManager.LoadScene("Scenes/BossTest"); // Set it to Boss Room
		    }
	    }

	    else if (card.name == "RaiseChaos") {
		    PlayerPrefs.SetInt("CurrentChaos", chaosLevel+1);
            PlayerPrefs.SetFloat("AttackMod", attackMod + 0.05f);
            loadSceneNow = true;
		    Debug.Log("Raise Chaos");
		    if (nextSceneNumber < 5) {
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
