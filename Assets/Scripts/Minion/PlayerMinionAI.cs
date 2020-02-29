using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMinionAI : MonoBehaviour
{
    public enum State {turretScouting, baseAttack, minionAttack, enemyAttack};

    public State state;
    

    Kunai kunaiScript;

    public Animator minionAnimator;

    GameObject target;
    GameObject turret1;
    GameObject enemyBase;
    

    NavMeshAgent agent;

    public bool isAttack;

    public float distBetweenMinionAndPlayer;
    public float distBetweenMinionAndTurret1;
    public float distBetweenMinionAndPlayerBase;
    public float lookRadius = 10f;
    public float stopRadius = 5f;
    float nextTimeShoot = 0f;


    private void Awake()
    {
        state = State.turretScouting;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Enemy");
        turret1 = GameObject.FindGameObjectWithTag("ET");
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase");
        
        kunaiScript = GetComponent<Kunai>();

        state = State.turretScouting;

    }

    void Update()
    {


        if (target != null)
        {

            distBetweenMinionAndPlayer = Vector3.Distance(transform.position, target.transform.position);

            if (distBetweenMinionAndPlayer < lookRadius)
            {
                state = State.enemyAttack;
            }else if(distBetweenMinionAndPlayer > lookRadius)
            {
                isAttack = false;
                state = State.turretScouting;
            }

        }else if (target == null)
        {
            agent.isStopped = false;
            if(turret1 != null)
            {
                state = State.turretScouting;
            }
            else
            {
                state = State.baseAttack;
            }
        }



        if (turret1 != null)
        {

            distBetweenMinionAndTurret1 = Vector3.Distance(transform.position, turret1.transform.position);

            if (distBetweenMinionAndTurret1 < stopRadius)
            {
                agent.isStopped = true;
                FaceTarget(turret1.transform.position);
                isAttack = true;
            }

        } else if(turret1 == null)
        {
            agent.isStopped = false;
            state = State.baseAttack;
        }

        
        distBetweenMinionAndPlayerBase = Vector3.Distance(transform.position, enemyBase.transform.position);


        switch (state)
        {
            // - TURRET - //
            case State.turretScouting:
                agent.SetDestination(turret1.transform.position);
                isAttack = false;

                if (distBetweenMinionAndTurret1 < lookRadius)
                {
                    FaceTarget(turret1.transform.position);
                    isAttack = true;
                }

                if(turret1 == null)
                {
                    state = State.baseAttack;
                }


                if (distBetweenMinionAndPlayer < lookRadius)
                {
                    state = State.enemyAttack;
                }else if(distBetweenMinionAndPlayer > lookRadius)
                {
                    state = State.turretScouting;
                }


                break;
            // - BASE - //
            case State.baseAttack:
                agent.SetDestination(enemyBase.transform.position);

                

                if (distBetweenMinionAndPlayerBase < lookRadius)
                {
                    FaceTarget(enemyBase.transform.position);
                    isAttack = true;
                }
                else
                {
                    agent.SetDestination(enemyBase.transform.position);
                }


                if (distBetweenMinionAndPlayer < lookRadius)
                {
                    state = State.enemyAttack;
                }
                else if (distBetweenMinionAndPlayer > lookRadius)
                {
                    state = State.baseAttack;
                }



                print("Attacking Base!"); 
               // face


                break;

            // - ENEMY - //
            case State.enemyAttack:
                agent.SetDestination(target.transform.position);
                FaceTarget(target.transform.position);

                if(distBetweenMinionAndPlayer < lookRadius)
                {
                    agent.SetDestination(target.transform.position);
                    FaceTarget(target.transform.position);
                    isAttack = true;

                }
                else if(distBetweenMinionAndPlayer > lookRadius)
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
