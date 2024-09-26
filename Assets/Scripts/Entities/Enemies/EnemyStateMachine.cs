using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Handles the state switching of the enemy
 
 */

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBaseState initalState;
    private EnemyBaseState activeState;

    public EnemyBaseState ActiveState
    {
        get { return activeState; }
        set { activeState = value; }
    }

    public EnemyBaseState InitalState
    {
        get { return initalState; }
        set { initalState = value; }
    }


    //initalise statemachine
    public void Init()
    {
        //Debug.Log("Initialise Statemachine");
        //Debug.Log(initalState.GetType());
        changeState(initalState);
    }
    void Start()
    {

    }

    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }

    }

    public void changeState(EnemyBaseState nextState)
    {
        //if no next state is called perform exit function
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = nextState;

        //If no active state reset to default. this should usually only run at the start
        if (activeState != null)
        {
            activeState.StateMachine = this;
            activeState.Enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
