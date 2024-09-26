using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the melee enemy should act when entering, exiting and during its chase state. Enemy is in chase state when chasing the player
 
 */

public class MeleePunchState : MeleeBaseState
{
    public override void Enter()
    {
        Debug.Log("Punch");
        Melee.enemyAttack();
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        Melee.Agent.destination = Melee.Player.transform.position;
    }



}
