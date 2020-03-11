using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMinionAI : MonoBehaviour
{
    public enum State {turretScouting, baseAttack, minionAttack, enemyAttack};

    public State state;

    Kunai kunaiScript;

    GameObject target;
    GameObject turret1;
    GameObject enemyBase;
    

    NavMeshAgent agent;

    public bool isAttack;

    public float distBetweenMinionAndPlayer;
    public float distBetweenMinionAndTurret1;
    public float distBetweenMinionAndtargetBase;
    public float range = 10f;
    public float stopRadius = 5f;
    float nextTimeShoot = 0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        turret1 = GameObject.FindGameObjectWithTag("Player");
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase");
        
        kunaiScript = GetComponent<Kunai>();

        state = State.turretScouting;

    }

    void Update()
    {


        if (target != null)
        {

            distBetweenMinionAndPlayer = Vector3.Distance(transform.position, target.transform.position);

            if (distBetweenMinionAndPlayer < range)
            {
                state = State.enemyAttack;
            }else if(distBetweenMinionAndPlayer > range)
            {
                isAttack = false;
                state = State.turretScouting;
            }

        }else if (target == null)
        {
            if(turret1 != null)
            {
                state = State.turretScouting;
            }
            else
            {
                state = State.baseAttack;
            }
        }



        
        distBetweenMinionAndtargetBase = Vector3.Distance(transform.position, enemyBase.transform.position);


        switch (state)
        {
            


            // - TURRET - //
            case State.turretScouting:
                agent.SetDestination(turret1.transform.position);
                isAttack = false;

                if (distBetweenMinionAndTurret1 > range)
                {
                    FaceTarget(turret1.transform.position);
                    isAttack = true;
                }

                if(turret1 == null)
                {
                    state = State.baseAttack;
                }

                break;


            // - BASE - //
            case State.baseAttack:
                agent.SetDestination(enemyBase.transform.position);

                

                if (distBetweenMinionAndtargetBase < range)
                {
                    FaceTarget(enemyBase.transform.position);
                    isAttack = true;
                }
                else
                {
                    agent.SetDestination(enemyBase.transform.position);
                }


                if (distBetweenMinionAndPlayer < range)
                {
                    state = State.enemyAttack;
                }
                else if (distBetweenMinionAndPlayer > range)
                {
                    state = State.baseAttack;
                }


                break;


            // - ENEMY - //
            case State.enemyAttack:
                agent.SetDestination(target.transform.position);
                FaceTarget(target.transform.position);

                if(distBetweenMinionAndPlayer < range)
                {
                    agent.SetDestination(target.transform.position);
                    FaceTarget(target.transform.position);
                    isAttack = true;

                }
                else if(distBetweenMinionAndPlayer > range)
                {
                    isAttack = false;
                    state = State.turretScouting;
                }
                

                break;

        }


        if (isAttack)
        {
            MinionAttack();
        }
        else if(isAttack == false)
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
