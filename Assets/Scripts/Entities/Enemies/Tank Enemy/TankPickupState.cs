using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its pickup state. Enemy is in pickup state when throwable object comes into pickup range as enemy is chasing player
 
 */
public class TankPickupState : TankBaseState
{
    public override void Enter()
    {
        Debug.Log("Picking up State");
        Tank.pickupThrowable(Tank.checkForThrowable());
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
    }
}
