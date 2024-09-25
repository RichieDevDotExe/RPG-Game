using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 
Author: Richard
Desc: Scriptable object to contain Tank specific values used when setting initial values using Init()
 
 */

[CreateAssetMenu(fileName = "TankEnemySetting", menuName = "EnemyValues/TankEnemyValues")]
public class TankValues : EnemyValues
{
    [SerializeField] public float pickUpRange;
    [SerializeField] public float throwDetectionRange;
    [SerializeField] public float throwDistance;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float chargeStrength;
    [SerializeField] public float chargeRange;
}
