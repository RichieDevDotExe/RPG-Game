using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its Charge state. Enemy is in charge state is enemy isn't close enough
to punch the player but still in range to lunge at player
 
 */

public class TankChargeState : TankBaseState
{
    public override void Enter()
    {
        Debug.Log("Starting Charge");
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
        Tank.enemyAttack(Tank.startCharge);
    }
}
