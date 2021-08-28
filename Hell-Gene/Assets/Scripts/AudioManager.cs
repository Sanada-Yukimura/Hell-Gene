using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private float volume;

	public AudioSource source;
    // Start is called before the first frame update
    void Start() {
	    volume = PlayerPrefs.GetFloat("GameVolume", 0.35f);
	    source.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
