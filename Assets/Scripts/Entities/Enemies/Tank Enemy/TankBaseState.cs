using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 
Author: Richard
Desc: Base tank state for all tank states to be based on
 
 */

public abstract class TankBaseState : EnemyBaseState
{
    public TankEnemy Tank
    {
        get { return (TankEnemy)Enemy; }
        set { Enemy = value; }
    }
}
