using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{
    //Lav en boolean system til hvad der kan skades og ikke skades

    public bool attack1_MinionBall;
    public bool attack2_Fireball;
    public bool attack3_Kunai;
    public bool attack4_Ultimate;

    int damage1;
    int damage2;
    int damage3;
    int damage4;

    void Update()
    {
        damage1 = Random.Range(5, 10);
        damage2 = Random.Range(8, 15);
        damage3 = Random.Range(25, 35);
        damage4 = Random.Range(50, 90);
    }

    void OnTriggerEnter(Collider other)
    {

        if (attack1_MinionBall)
        {
            if (other.gameObject.CompareTag("1"))
            {
                gameObject.GetComponent<Health>().TakeDamage(damage1);
                Destroy(other.gameObject);
            }

        } else{
            //Do nothing
        }
        if (attack2_Fireball)
        {
            if (other.gameObject.CompareTag("2"))
            {
                gameObject.GetComponent<Health>().TakeDamage(damage2);
                Destroy(other.gameObject);
            }

        }
        else
        {
            //Do nothing
        }

        if (attack3_Kunai)
        {
            if (other.gameObject.CompareTag("3"))
            {
                gameObject.GetComponent<Health>().TakeDamage(damage3);
                Destroy(other.gameObject);
            }

        }
        else
        {
            //Do nothing
        }

        if (attack4_Ultimate)
        {
            if (other.gameObject.CompareTag("4"))
            {
                gameObject.GetComponent<Health>().TakeDamage(damage4);
            }

        }
        else
        {
            //Do nothing
        }

    }
}
