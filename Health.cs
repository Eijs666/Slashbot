using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float healAmount = 5f;
    float currentHealth;
    float healingRate = 5f;
    float nextHealingTime = 0f;

    public Image healthBar;


    //Health = 100 at start
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //If object is hurt Get 2 health points for every second - else if currenthealt == maxhealth { //dont do anything}
        if (Time.time >= nextHealingTime && currentHealth < maxHealth)
        {
            currentHealth += healAmount;
            nextHealingTime = Time.time + healingRate;
            healthBar.fillAmount = currentHealth / maxHealth;
            print("Healing");
        }

        if (currentHealth <= 0)
        {
            Die();
        }


    }

    //Object takes damge
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / maxHealth;

    }

    void Die()
    {
        print("Enemy Died");
        Destroy(gameObject);
    }


}
