using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateScript : MonoBehaviour
{
    #region Variables

    public BoxCollider myCollider;

    public float timeToDestroy = 4f;

    private GameObject enemy;
    //private GameObject player;
    private GameObject enemyBase;

    private GameObject playerBase;

    public int damage = 80;

    #endregion

    private void Start()
    {
        //Get box collider component
        myCollider = GetComponent<BoxCollider>();
        //Wait for box collider to get active
        myCollider.enabled = false;
        Invoke("TurnOnCollider", 0.5f);
       


    }


    void Update()
    {
       
        //Destroy Kunai after some time
        Invoke("DestroyUltimate", timeToDestroy);
        enemy = GameObject.FindGameObjectWithTag("Enemy");
       // player = GameObject.FindGameObjectWithTag("Player");
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase");
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");

    }
    
    void TurnOnCollider()
    {
        myCollider.enabled = true;
    }

    //Enemydetection
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyTakeDamage();
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            DestroyUltimate();
        }

        /*
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerTakeDamage();
        }*/
        

        if (other.gameObject.CompareTag("EnemyBase"))
        {
            EnemyBaseTakeDamage();
        }
        if (other.gameObject.CompareTag("PlayerBase"))
        {
            PlayerBaseTakeDamage();
        }

    }

    /*
    void PlayerTakeDamage()
    {
        player.GetComponent<Health>().TakeDamage(damage);
        DestroyUltimate();
    }*/

    void EnemyTakeDamage()
    {
        enemy.GetComponent<Health>().TakeDamage(damage);
      //  DestroyUltimate();
    }

    void PlayerBaseTakeDamage()
    {
        playerBase.GetComponent<Health>().TakeDamage(damage);
        DestroyUltimate();
    }

    void EnemyBaseTakeDamage()
    {
        enemyBase.GetComponent<Health>().TakeDamage(damage);
        DestroyUltimate();
    }


    void DestroyUltimate()
    {
        Destroy(gameObject);
    }
}
