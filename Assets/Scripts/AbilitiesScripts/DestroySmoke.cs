using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySmoke : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy_Smoke", 1f);
    }


    public void Destroy_Smoke()
    {
        Destroy(gameObject);
    }
}
