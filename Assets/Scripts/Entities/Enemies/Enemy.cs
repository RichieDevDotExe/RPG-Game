using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Entity
{
    [Header("Enemy Stats")]
    [SerializeField] protected EnemyStateMachine stateMachine;
    [SerializeField] protected Action<GameObject> destroyThis;
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected BoxCollider hitbox;
    [SerializeField] protected Player player;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float fieldOfView;
    [SerializeField] protected Transform[] waypoints;
    public NavMeshAgent Agent { get => agent; }
    public Transform[] Waypoints { get => waypoints; }
    public Player Player { get => player; }
    public float AttackRange { get => attackRange; }

    public abstract void Init();

    public virtual bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray lineOfSight = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(lineOfSight, out hitInfo, detectionRange))
                    {
                        if (hitInfo.transform.gameObject == player.gameObject)
                        {
                            //Debug.Log("I SEE YOU");
                            return true;
                        }
                    }
                    Debug.DrawRay(lineOfSight.origin, lineOfSight.direction * detectionRange);
                }
            }
        }
        //Debug.Log("missing");
        return false;
    }
    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    public void resetThis()
    {
        destroyThis(transform.parent.gameObject);
        Init();
    }

    public abstract void enemyAttack();
}
