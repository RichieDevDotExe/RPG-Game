using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyBaseState initalState;
    private EnemyBaseState activeState;

    public EnemyBaseState ActiveState
    {
        get { return activeState; }
        set { activeState = value; }
    }


    //initalise statemachine
    public void Init()
    {
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
