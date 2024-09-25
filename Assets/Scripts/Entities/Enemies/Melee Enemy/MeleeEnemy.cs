using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{

    //tank stats
    private MeleeValues meleeValues;
    public override void enemyAttack(Action calledAttack = null)
    {
        throw new NotImplementedException();
    }

    public override void entityTakeDamage(float damage)
    {
        health -= damage;
    }

    public override void Init()
    {
        meleeValues = (MeleeValues)enemyValues;
        Debug.Log("Init" + name);
        maxHealth = meleeValues.maxHealth;
        health = maxHealth;
        speed = meleeValues.speed;
        damage = meleeValues.damage;
        detectionRange = meleeValues.detectionRange;
        fieldOfView = meleeValues.fieldOfView;
        attackRange = meleeValues.attackRange;

        //include code to get enemy path
        Array.Clear(waypoints, 0, waypoints.Length);
        AddDescendantsWithTag(transform.parent.transform.Find("Waypoint List").transform, "waypoint");
        foreach (Transform x in waypoints)
        {
            Debug.Log(x.name);
        }
        stateMachine.InitalState = new TankIdleState();
        //Debug.Log(stateMachine.InitalState.GetType());
        stateMachine.Init();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void entityAttack()
    {
        throw new NotImplementedException();
    }

    protected override void entityDie()
    {
        destroyThis(transform.parent.gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Transform[0];
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
