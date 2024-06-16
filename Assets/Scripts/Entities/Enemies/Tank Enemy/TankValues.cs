using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "TankEnemySetting", menuName = "EnemyValues/TankEnemyValues")]
public class TankValues : EnemyValues
{
    [SerializeField] public float maxSpeed;
    [SerializeField] public float chargeStrength;
}
