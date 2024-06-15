using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIdleState : EnemyBaseState
{
    private int waypointIndex;

    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform()
    {
        throw new System.NotImplementedException();
    }

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
    }
}
