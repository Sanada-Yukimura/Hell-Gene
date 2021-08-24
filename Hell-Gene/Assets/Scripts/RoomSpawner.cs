using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openDirection;
    // 1 - needs bottom door
    // 2 - needs right door
    // 3 - needs top door
    // 4 - needs left door
    private RoomTemplates templates;
    private int rando;
    public bool spawned = false;
    void Start() {
	    templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
	    Invoke("SpawnRooms", 3f);
    }
    
    void SpawnRooms() {
	    if (spawned == false) {
		    if (openDirection == 1) {
			    rando = Random.Range(0, 1);
			    Instantiate(templates.downRooms[rando], transform.position, Quaternion.identity);
		    
		    }
		    else if (openDirection == 2) {
			    rando = Random.Range(0, 1);
			    Instantiate(templates.rightRooms[rando], transform.position, Quaternion.identity);
		    
		    }
		    else if (openDirection == 3) {
			    rando = Random.Range(0, 1);
			    Instantiate(templates.upRooms[rando], transform.position, Quaternion.identity);
		    }
		    else if (openDirection == 4) {
			    rando = Random.Range(0, 1);
			    Instantiate(templates.leftRooms[rando], transform.position, Quaternion.identity);
		    }

		    spawned = true;
	    }

    }

    void OnTriggerEnter2D(Collider2D collider) {
	    if (collider.CompareTag("Spawnpoint")) {
		    Destroy(gameObject);
	    }
    }
}
