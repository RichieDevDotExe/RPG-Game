using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

/*
 
Author: Richard
Desc: Contains stats and functions all enemies must have
 
 */


public abstract class Enemy : Entity
{
    [Header("Enemy Stats")]
    protected EnemyStateMachine stateMachine;
    [SerializeField] protected EnemyValues enemyValues;
    protected Action<GameObject> destroyThis;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Collider hitbox;
    protected Player player;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float fieldOfView;
    protected Transform[] waypoints;
    public NavMeshAgent Agent { get => agent; }
    public Transform[] Waypoints { get => waypoints; }
    public Player Player { get => player; }
    public float AttackRange { get => attackRange; }

    //Sets the starting values of enemy stats
    public abstract void Init();

    //Default player detection for the enemy
    public virtual bool CanSeePlayer()
    {
        if (player != null)
        {
            //Debug.Log("Player exist");
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
                        //Debug.Log("hit = "+ hitInfo.transform.gameObject.name);
                        //Debug.Log("player = " + player.gameObject.name);
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

    //passes function on how to handle enemy when sending back to object pool
    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    //resets enemy when being brought out of pool
    public void resetThis()
    {
        destroyThis(transform.parent.gameObject);
        Init();
    }

    //enemy attack
    public abstract void enemyAttack(Action calledAttack = null);

    //creates waypoint list for enemy to follow
    public virtual void AddDescendantsWithTag(Transform parent, string tag)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                children.Add(child.transform);
            }
            AddDescendantsWithTag(child, tag);
        }
        waypoints = children.ToArray();
    }

}
