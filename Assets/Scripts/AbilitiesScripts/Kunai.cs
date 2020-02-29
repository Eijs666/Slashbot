using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Transform attackPoint;
    public Transform enemyAttackPoint;
    public ThrowScript kunaiThrowScript;
    public ThrowScript fireballScript;
    public UltimateScript ultimateScript;
    public MinionMagicBall minionBallScript;



    public void PlayerThrow()
    {
        //Instantiate Kunai
        ThrowScript newThrowScript = Instantiate(kunaiThrowScript, attackPoint.position, attackPoint.rotation) as ThrowScript;
    }


    public void Fireball()
    {
        //Instantiate Kunai
        ThrowScript newThrowScript = Instantiate(fireballScript, attackPoint.position, attackPoint.rotation) as ThrowScript;
    }

    public void Ultimate()
    {
        UltimateScript newUltimateScript = Instantiate(ultimateScript, attackPoint.position, attackPoint.rotation) as UltimateScript;
        //GameObject newGameObject = Instantiate(ultimate, attackPoint.position, attackPoint.rotation) as GameObject;
    }


    public void EnemyThrow()
    {
        //Instantiate Kunai
        ThrowScript newThrowScript = Instantiate(kunaiThrowScript, enemyAttackPoint.position, enemyAttackPoint.rotation) as ThrowScript;
    }


    public void MinionThrow()
    {
        //Instantiate Kunai
        MinionMagicBall newMinionMagicBall = Instantiate(minionBallScript, attackPoint.position, attackPoint.rotation) as MinionMagicBall;
    }

}
