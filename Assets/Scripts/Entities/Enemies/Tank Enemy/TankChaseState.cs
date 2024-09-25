using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its chase state. Enemy is in chase state when chasing the player
 
 */

public class TankChaseState : TankBaseState
{
    public override void Enter()
    {
        Debug.Log("Chasing");
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        ChaseLogic();
    }

    //if in chase state sets nav agent destination to enemy.
    private void ChaseLogic()
    {
        Tank.Agent.destination = Tank.Player.transform.position;
        //needs to check if player is too far away to see so it can go back to idle
        if (Tank.CanSeePlayer() != true)
        {
            StateMachine.changeState(new TankIdleState());
        }
        else
        {
            Tank.attackChecker();
        }
    }

}
