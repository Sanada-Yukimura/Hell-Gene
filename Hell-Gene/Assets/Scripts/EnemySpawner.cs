using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyTemplates templates;
    private GameObject player;
    private int rando;
    public bool spawned = false;
    public int chaosLevel;
    public int chaosRequirement; //requirement for chaos level to spawn enemy

    public float waitTime = 3f;


    // Start is called before the first frame update
    void Start()
    {
        chaosLevel = GameObject.FindGameObjectWithTag("ChaosHandler").GetComponent<ChaosHandler>().chaosLevel;
        templates = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyTemplates>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    void SpawnEnemy()
    {
        if (!spawned && chaosLevel >= chaosRequirement && Vector2.Distance(player.transform.position, transform.position) <= 5)
        {
            rando = Random.Range(0, templates.enemyTypes.Length);
            Instantiate(templates.enemyTypes[rando], transform.position, Quaternion.identity);
            spawned = true;
            Destroy(gameObject, waitTime); //destroys object after time period
        }
    }
    //private void OnDrawGizmos() // Display hitboxes and ranges
    //{
    //    Gizmos.color = Color.green; // Display detection range
    //    Gizmos.DrawWireSphere(transform.position, 6);
    //}
}
