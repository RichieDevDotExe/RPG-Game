using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its melee state. Enemy is in melee state when player is in close range
 
 */

public class TankMeleeState : TankBaseState
{
    public override void Enter()
    {
        Debug.Log("Attacking");
        AttackingLogic();
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {

    }

    protected void AttackingLogic()
    {
        Tank.enemyAttack(Tank.startPunch);
    }
}
