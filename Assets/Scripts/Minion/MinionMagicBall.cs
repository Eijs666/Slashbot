using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMagicBall : MonoBehaviour
{
    #region Variables
    public float throwSpeed = 45;
    public float timeToDestroy = 4f;

    private GameObject pt;
    //private GameObject et;
    private GameObject enemy;
    private GameObject player;


    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public int damage;
    #endregion

    void Start()
    {
        
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        //Bases

        //Turret
        pt = GameObject.FindGameObjectWithTag("PT");

    }

    void Update()
    {
        damage = Random.Range(5, 15);
        //Movement of Kunai
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed);

        //Destroy Kunai after some time
        Invoke("DestroyKunai", timeToDestroy);
  

    }

    //Enemydetection
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerTakeDamage();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyTakeDamage();
        }

        if (other.gameObject.CompareTag("PT"))
        {
            PTTakeDamage();
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            DestroyKunai();
        }

    }

    void PlayerTakeDamage()
    {
        player.GetComponent<Health>().TakeDamage(damage);
        DestroyKunai();
    }
    void EnemyTakeDamage()
    {
        enemy.GetComponent<Health>().TakeDamage(damage);
        DestroyKunai();
    }

    void PTTakeDamage()
    {
        pt.GetComponent<Health>().TakeDamage(damage);
        DestroyKunai();
    }

    //Destroy function
    void DestroyKunai()
    {
        Destroy(gameObject);
    }
}
