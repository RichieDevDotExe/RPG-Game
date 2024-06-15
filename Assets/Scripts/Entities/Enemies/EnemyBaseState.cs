using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    private Enemy enemy;
    private EnemyStateMachine stateMachine;

    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    public EnemyStateMachine StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    //different functions to be called when enemy enters, is currently and exiting the state
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}