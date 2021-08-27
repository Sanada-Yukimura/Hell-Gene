using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    /* Code taken from Blackthornpod's Random Dungeon Generation Tutorial Series */

    public GameObject[] downRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    public GameObject staircase;
    public StairChecker staircheck;

    private bool stairSpawned = false;

    void Update()
    {
        if (waitTime <= 0)
        {
            GameObject finalRoom = rooms[rooms.Count - 1];
            staircheck = finalRoom.transform.Find("StairSpawn").GetComponent<StairChecker>();
            if (!stairSpawned)
            {
                staircheck.spawnStaircase(staircase);
                stairSpawned = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
