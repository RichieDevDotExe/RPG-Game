using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Base melee state for all melee states to be based on
 
 */

public abstract class MeleeBaseState : EnemyBaseState
{
    public MeleeEnemy Melee
    {
        get { return (MeleeEnemy)Enemy; }
        set { Enemy = value; }
    }
}
