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

    void Update()
    {
        switch (openDirection)
        {
            case 1:
                //spawn a room with BOTTOM door
                break;
            case 2:
                //spawn a room with RIGHT door
                break;
            case 3:
                //spawn a room with TOP door
                break;
            case 4:
                //spawn a room with LEFT door
                break;
        }
    }
}
