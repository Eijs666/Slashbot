using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    #region Variables
    public float throwSpeed = 45;
    public float timeToDestroy = 4f;

    public float attackRange = 0.5f;
    public float attackRate = 2f;


    #endregion


    void Update()
    {
        //Movement of Kunai
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed);

        //Destroy Kunai after some time
         Invoke("DestroyKunai", timeToDestroy);

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }



    //Destroy function
    void DestroyKunai()
    {
        Destroy(gameObject);
    }
}
