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
	    Invoke("SpawnRooms", 0.1f);
    }
    
    void SpawnRooms() {
	    if (spawned == false) {
		    if (openDirection == 1) {
			    rando = Random.Range(0, templates.downRooms.Length);
			    Instantiate(templates.downRooms[rando], transform.position, templates.downRooms[rando].transform.rotation);
		    }
		    else if (openDirection == 2) {
			    rando = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rando], transform.position, templates.leftRooms[rando].transform.rotation);
		    }
		    else if (openDirection == 3) {
			    rando = Random.Range(0, templates.topRooms.Length);
			    Instantiate(templates.topRooms[rando], transform.position, templates.topRooms[rando].transform.rotation);
		    }
		    else if (openDirection == 4) {
			    rando = Random.Range(0, templates.rightRooms.Length);
			    Instantiate(templates.rightRooms[rando], transform.position, templates.rightRooms[rando].transform.rotation);
		    }
		    spawned = true;
	    }

    }

    void OnTriggerEnter2D(Collider2D collider) {
	    if (collider.CompareTag("Spawnpoint")) {
            if(collider.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
	            Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
	            Destroy(gameObject);

            }
            spawned = true;
	    }
    }
}
