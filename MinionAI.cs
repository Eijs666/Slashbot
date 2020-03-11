using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{
    public enum State { turretScouting, baseAttack, enemyAttack };

    public State state;

    Kunai kunaiScript;

    public Transform target;
    GameObject turret1;
    GameObject playerBase;

    NavMeshAgent agent;

    public bool isAttack;

    public float distBetweenMinionAndPlayer;
    public float distBetweenMinionAndTurret1;
    public float distBetweenMinionAndPlayerBase;
    public float lookRadius = 10f;
    public float stopRadius = 5f;
    float nextTimeShoot = 0f;

    public string targetTag = "Player";

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        turret1 = GameObject.FindGameObjectWithTag("PT");
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");

        kunaiScript = GetComponent<Kunai>();

        InvokeRepeating("UpdateTarget", 1f, 1f);

        state = State.turretScouting;

    }


    void UpdateTarget()
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

        if (nearestEnemy != null && shortestDistance <= lookRadius)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }


    }


    void Update()
    {
      /*  if (target == null)
        {
            return;
        }         */
         distBetweenMinionAndPlayer = Vector3.Distance(transform.position, target.transform.position);





        if (turret1 != null)
        {

            distBetweenMinionAndTurret1 = Vector3.Distance(transform.position, turret1.transform.position);

        }
        else if (turret1 == null)
        {
            state = State.baseAttack;
        }

        distBetweenMinionAndPlayerBase = Vector3.Distance(transform.position, playerBase.transform.position);


        switch (state)
        {

            // - TURRET - //
            case State.turretScouting:


                if(distBetweenMinionAndPlayer < lookRadius)
                {
                    FaceTarget(target.transform.position);
                    isAttack = true;
                }
                else if(distBetweenMinionAndPlayer > lookRadius)
                {
                    agent.SetDestination(turret1.transform.position);
                    isAttack = false;
                }
                
                break;


            // - BASE - //
            case State.baseAttack:
                agent.SetDestination(playerBase.transform.position);


                break;
            
                
            // - ENEMY - //
            case State.enemyAttack:
                agent.SetDestination(target.transform.position);
                if(distBetweenMinionAndPlayer< lookRadius)
                {
                    FaceTarget(target.transform.position);
                    isAttack = true;
                }
                else
                {
                    isAttack = false;
                }
                      


                break;


        }


        if (isAttack)
        {
            MinionAttack();
        }
        else if (isAttack == false)
        {

        }

    }

    void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.0f);
    }

    void MinionAttack()
    {
        if (Time.time >= nextTimeShoot)
        {
            kunaiScript.MinionThrow();
            nextTimeShoot = Time.time + 2f;
        }
    }

}