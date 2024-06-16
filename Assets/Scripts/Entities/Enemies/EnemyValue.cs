using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyValues : ScriptableObject
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float attackRange;
    [SerializeField] public float detectionRange;
    [SerializeField] public float fieldOfView;
    [SerializeField] public float enemyCooldown;
    [SerializeField] public float iFrames;
}
 