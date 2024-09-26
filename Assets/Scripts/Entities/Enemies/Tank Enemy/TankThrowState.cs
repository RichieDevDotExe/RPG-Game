using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its throwing state. Enemy is in throwing state when player is in throwing range AND is
currently holding a throwable object
 
 */

public class TankThrowState : TankBaseState
{
    public override void Enter()
    {
        Debug.Log("throwing State");
        Tank.startThrow();
        //Debug.Log("Thrown");
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
    }


}
