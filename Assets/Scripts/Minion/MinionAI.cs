using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{
    public enum State {turretScouting, baseAttack, enemyAttack};

    public State state;
    

    Kunai kunaiScript;

    public Animator minionAnimator;

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

    private void Awake()
    {
        state = State.turretScouting;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        turret1 = GameObject.FindGameObjectWithTag("PT");
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");
        
        kunaiScript = GetComponent<Kunai>();

        state = State.turretScouting;

        InvokeRepeating("UpdateTarget", 1f, 1f);
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

       /* if (player != null)
        {

            distBetweenMinionAndPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distBetweenMinionAndPlayer < lookRadius)
            {
                state = State.enemyAttack;
            }else if(distBetweenMinionAndPlayer > lookRadius)
            {
                isAttack = false;
                state = State.turretScouting;
            }

        }else if (player == null)
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
        */


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
        
        distBetweenMinionAndPlayerBase = Vector3.Distance(transform.position, playerBase.transform.position);


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
                agent.SetDestination(playerBase.transform.position);


                if (distBetweenMinionAndPlayerBase < lookRadius)
                {
                    FaceTarget(playerBase.transform.position);
                    isAttack = true;
                }
                else
                {
                    agent.SetDestination(playerBase.transform.position);
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
          /*      agent.SetDestination(player.transform.position);
                FaceTarget(player.transform.position);

                if(distBetweenMinionAndPlayer < lookRadius)
                {
                    agent.SetDestination(player.transform.position);
                    FaceTarget(player.transform.position);
                    isAttack = true;

                }
                else if(distBetweenMinionAndPlayer > lookRadius)
                {
                    isAttack = false;
                    state = State.turretScouting;
                }*/
                

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
