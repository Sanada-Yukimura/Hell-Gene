using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour {
	private static AudioSource thisAudioSource;
	void Awake() {
		GameObject[] numAudioSources = GameObject.FindGameObjectsWithTag("NormalMusic");
		if (numAudioSources.Length != 1) {
			Destroy(this.gameObject);
		}
		else {
			DontDestroyOnLoad(transform.gameObject);
		}
		thisAudioSource = GetComponent<AudioSource>();
		
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic() {
	    if (thisAudioSource.isPlaying) {
		    return;
	    }
	    thisAudioSource.Play();
    }

    public void StopMusic() {
	    thisAudioSource.Stop();
    }
    
    
    
}
