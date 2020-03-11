using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public float attackRate = 10f;
    float nextAttackTime = 0f;
    public GameObject smoke;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                VisibilityDisable();
                Invoke("VisibilityEnable", 3f);
                nextAttackTime = Time.time * attackRate / 0.5f;
            }
        }
       
    }

    

    public void VisibilityEnable()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        Smoke();

    }
    public void VisibilityDisable()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.enabled = false;
        Smoke();

    }

    public void Smoke()
    {
        Instantiate(smoke, transform.position, transform.rotation);
        
    }

    
}