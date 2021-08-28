using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NameGenerator : MonoBehaviour {
	public String[] firstNames;

	public String[] lastNames;

	public TMP_Text bossName;
    // Start is called before the first frame update
    void Start() {
	    bossName = GetComponent<TMP_Text>();
	    int firstNameNumber = Random.Range(0, firstNames.Length);
	    int lastNameNumber = Random.Range(0, lastNames.Length);
	    bossName.text = firstNames[firstNameNumber] +" "+ lastNames[lastNameNumber];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
