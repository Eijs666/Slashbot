using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFinalMinionAI : MonoBehaviour
{
    public enum State { turretScouting, baseAttack, enemyAttack };
    public State state;

    Kunai kunaiScript;
    NavMeshAgent agent;
    public Transform target;
    public GameObject turret1;
    public GameObject targetBase;
    public string targetTag = "Enemy";

    public bool isAttack;

    float range = 200f;
    public float lookRadius = 30f;
    public float distBetweenMinionAndTarget;
    public float distBetweenMinionAndTurret1;
    public float distBetweenMinionAndTargetBase;
    float nextTimeShoot = 0f;


    void Start()
    {
        kunaiScript = GetComponent<Kunai>();
        agent = GetComponent<NavMeshAgent>();
        turret1 = GameObject.FindGameObjectWithTag("ET");
        targetBase = GameObject.FindGameObjectWithTag("EnemyBase");
        InvokeRepeating("UpdateTarget", 0f, 0.1f);

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
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

    }



    void Update()
    {
        // UpdateTarget();
        if (target == null)
        {
            return;
        }

        if (turret1 != null)
        {
            distBetweenMinionAndTurret1 = Vector3.Distance(transform.position, turret1.transform.position);
        }

        if (turret1 == null)
        {
            state = State.baseAttack;
        }



        distBetweenMinionAndTarget = Vector3.Distance(transform.position, target.transform.position);
        distBetweenMinionAndTargetBase = Vector3.Distance(transform.position, targetBase.transform.position);

        if (distBetweenMinionAndTarget <= lookRadius)
        {
            state = State.enemyAttack;
        }

        switch (state)
        {
            // - TURRET - //
            case State.turretScouting:
                if (turret1 != null)
                {
                    //distBetweenMinionAndTurret1 = Vector3.Distance(transform.position, turret1.transform.position);
                    agent.SetDestination(turret1.transform.position);
                    if (distBetweenMinionAndTurret1 <= lookRadius)
                    {
                        FaceTarget(turret1.transform.position);
                        isAttack = true;
                    }
                    else if (distBetweenMinionAndTurret1 > lookRadius)
                    {
                        isAttack = false;
                        agent.SetDestination(turret1.transform.position);

                    }


                    if (distBetweenMinionAndTarget <= lookRadius)
                    {
                        state = State.enemyAttack;
                    }

                }
                else if (turret1 == null)
                {
                    state = State.baseAttack;
                }

                break;


            // - BASE - //
            case State.baseAttack:
                agent.SetDestination(targetBase.transform.position);
                if (distBetweenMinionAndTargetBase < lookRadius)
                {
                    //attack
                    FaceTarget(targetBase.transform.position);
                    isAttack = true;

                }
                if (distBetweenMinionAndTarget <= lookRadius)
                {
                    state = State.enemyAttack;
                }

                break;


            // - ENEMY - //
            case State.enemyAttack:
                agent.SetDestination(target.transform.position);
                if (distBetweenMinionAndTarget < lookRadius)
                {
                    //Attack Target
                    FaceTarget(target.transform.position);
                    isAttack = true;
                }
                else if (distBetweenMinionAndTarget > lookRadius)
                {
                    if (turret1 != null)
                    {

                        state = State.turretScouting;
                    }
                    else
                    {
                        state = State.baseAttack;
                    }
                }

                break;
        }


        if (isAttack)
        {
            MinionAttack();
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