using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadScript : MonoBehaviour
{
    public float distanceBetweenTurretAndEnemy;
    public float distanceBetweenTurretAndMinion;
    readonly float turretRadius = 20f;

    //  GameObject target;
    // GameObject minion;

    /*
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy");
        minion = GameObject.FindGameObjectWithTag("EnemyMinion");
    }

    // Update is called once per frame
    void Update()
    {
        //Distance between player and turret in float
        distanceBetweenTurretAndEnemy = Vector3.Distance(transform.position, target.transform.position);
        distanceBetweenTurretAndMinion = Vector3.Distance(transform.position, minion.transform.position);

        if (distanceBetweenTurretAndMinion < turretRadius)
        {
            transform.LookAt(minion.transform.position, Vector3.up);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * turretRadius, Color.red);
            //attack minion


        } else if(distanceBetweenTurretAndEnemy < turretRadius)
        {
            //Lookat gameobject
            transform.LookAt(target.transform.position, Vector3.up);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * turretRadius, Color.red);
            
            //Attack gameobject for every seconds

        }

        
        
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawWireSphere(transform.position, turretRadius);
    }
}
*/
}