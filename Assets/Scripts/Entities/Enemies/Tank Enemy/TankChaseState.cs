using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankChaseState : EnemyBaseState
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
        Enemy.Agent.destination = Enemy.Player.transform.position;
        Enemy.Agent.speed = Enemy.EntitySpeed;
        //needs to check if player is too far away to see so it can go back to idle
        if (Enemy.CanSeePlayer() != true)
        {
            StateMachine.changeState(new TankIdleState());
        }
        //or close enough to attack 
        if (Vector3.Distance(Enemy.transform.position, Enemy.Player.transform.position) <= Enemy.AttackRange)
        {
            StateMachine.changeState(new TankAttackState());
        }
    }

}
