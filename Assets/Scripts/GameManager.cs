using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyMinions;
    public GameObject[] playerMinions;

    public Transform enenmyMinionSpawnPoint;
    public Transform playerMinionSpawnPoint;
     

    

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("InstantiateEnemyMinions", 1f, 30f);
        InvokeRepeating("InstantiatePlayerMinions", 1f, 30f);

    }


    public void InstantiateEnemyMinions()
    {
        for (int i = 0; i < enemyMinions.Length; i++){

            GameObject newMinion = Instantiate(enemyMinions[i]) as GameObject;

            newMinion.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(enenmyMinionSpawnPoint.position);
        }
    }
    public void InstantiatePlayerMinions()
    {
        for (int i = 0; i < playerMinions.Length; i++)
        {

            GameObject newPlayerMinion = Instantiate(playerMinions[i]) as GameObject;

            newPlayerMinion.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(playerMinionSpawnPoint.position);
        }
    }


}
