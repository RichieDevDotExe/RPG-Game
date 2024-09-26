using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the melee enemy should act when entering, exiting and during its chase state. Enemy is in chase state when chasing the player
 
 */

public class MeleeChaseState : MeleeBaseState
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
        Melee.Agent.destination = Melee.Player.transform.position;
        //needs to check if player is too far away to see so it can go back to idle
        if (Melee.CanSeePlayer() != true)
        {
            StateMachine.changeState(new MeleeIdleState());
        }
        else
        {
            Melee.attackChecker();
        }
    }

}
