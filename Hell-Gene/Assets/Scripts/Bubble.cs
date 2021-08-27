using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float damageInterval;
    public float despawnTimer;
    public float damage;
    private bool firstProc = false;

    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0) Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemycollider")
        {
            if (despawnTimer % damageInterval <= 0.033 || !firstProc) collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            firstProc = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
