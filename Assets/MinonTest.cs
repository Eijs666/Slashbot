using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinonTest : MonoBehaviour
{
    private Transform target;
    public float range = 10f;

    public string targetTag = "Player";

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }



    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, range);

    }
}
