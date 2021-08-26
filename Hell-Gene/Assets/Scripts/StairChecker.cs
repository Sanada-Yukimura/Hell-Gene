using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairChecker : MonoBehaviour
{

    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnStaircase(GameObject staircase)
    {
        if (!spawned)
        {
            Instantiate(staircase, transform.position, Quaternion.identity);
            spawned = true;
        }
    }
}
