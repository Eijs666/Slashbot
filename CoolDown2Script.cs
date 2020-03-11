using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown2Script : MonoBehaviour
{
    public string abilityName;
    public Image ability;
    float cooldown;
    bool isCooldown;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cooldown = 3f;
            isCooldown = true;
            ability.fillAmount = 0;

        }
        if (isCooldown)
        {
            ability.fillAmount += 1 / cooldown * Time.deltaTime;

        }


    }

}
 