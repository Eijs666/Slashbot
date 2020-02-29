using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown3Script : MonoBehaviour
{
    public string abilityName;
    public Image ability;
    float cooldown;
    bool isCooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 10;
            isCooldown = true;
            ability.fillAmount = 0;

        }
        if (isCooldown)
        {
            ability.fillAmount += 1 / cooldown * Time.deltaTime;

        }


    }

}
 