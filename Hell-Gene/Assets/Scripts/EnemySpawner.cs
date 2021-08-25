using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyTemplates templates;
    private int rando;
    public bool spawned = false;
    public int chaosLevel;
    public int chaosRequirement; //requirement for chaos level to spawn enemy


    // Start is called before the first frame update
    void Start()
    {
        chaosLevel = GameObject.FindGameObjectWithTag("ChaosHandler").GetComponent<ChaosHandler>().chaosLevel;
        templates = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyTemplates>();
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (spawned == false && chaosLevel >= chaosRequirement)
        {
            rando = Random.Range(0, templates.enemyTypes.Length);
            Instantiate(templates.enemyTypes[rando], transform.position, Quaternion.identity);
            spawned = true;
        }
    }
}
