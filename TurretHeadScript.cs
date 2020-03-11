using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadScript : MonoBehaviour
{
    KunaiEnemy kunaiScript;

    public float turretRadius = 20f;
    public float fireRate = 0.5f;
    public float distanceBetweenTurretAndTarget;
    float nextTimeShoot = 0f;

    public bool isAttack;

    public Transform target;
    public string targetTag = "Player";

    void Start()
    {
        kunaiScript = GetComponent<KunaiEnemy>();

        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void Update()
    {

        distanceBetweenTurretAndTarget = Vector3.Distance(transform.position, target.transform.position);


        if (distanceBetweenTurretAndTarget < turretRadius)
        {
            FaceTarget(target.transform.position);
            isAttack = true;

        }
        else
        {
            isAttack = false;
            return;
        }


        if (isAttack)
        {
            TurretAttack();
        }


    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }
        if (nearestEnemy != null && shortestDistance <= 150f)
        {
            target = nearestEnemy.transform;
        }

    }

    void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.0f);
    }

    void TurretAttack()
    {
        if (Time.time >= nextTimeShoot)
        {
            kunaiScript.MinionThrow();
            nextTimeShoot = Time.time + fireRate;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, turretRadius);
    }
}