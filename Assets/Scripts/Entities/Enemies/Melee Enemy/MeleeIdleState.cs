using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Describes how the Tank enemy should act when entering, exiting and during its Idle state. Enemy is in idle state when player isn't detected
 
 */
public class MeleeIdleState : MeleeBaseState
{
    private int waypointIndex;

    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        IdleState();

        //Debug.Log("performing Idle");
    }

    //possibly move this to tank enemy
    public void IdleState()
    {
        if (Enemy.Agent.remainingDistance < 0.2f)
        {
            if (waypointIndex < Enemy.Waypoints.Length - 1)
            {
                waypointIndex += 1;
            }
            else
            {
                waypointIndex = 0;
            }
            Enemy.Agent.SetDestination(Enemy.Waypoints[waypointIndex].position);
        }
        //switches to chase state if player can be seen
        if (Enemy.CanSeePlayer() == true)
        {
            StateMachine.changeState(new MeleeChaseState());
        }
    }
}
