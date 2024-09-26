using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{

    //Melee stats
    private MeleeValues meleeValues;

    [Header("Melee Attack Hitboxes")]
    [SerializeField] private Collider punchHitbox;

    public override void enemyAttack(Action calledAttack = null)
    {
        entityAttack();
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
        player = Player.instance;

        //include code to get enemy path
        Array.Clear(waypoints, 0, waypoints.Length);
        AddDescendantsWithTag(transform.parent.transform.Find("Waypoint List").transform, "waypoint");
        foreach (Transform x in waypoints)
        {
            Debug.Log(x.name);
        }
        stateMachine.InitalState = new MeleeIdleState();
        //Debug.Log(stateMachine.InitalState.GetType());
        stateMachine.Init();
        agent = GetComponent<NavMeshAgent>();
    }

    //Functions used during the punch state of enemy. duringPunch() and finishPunch() run using animation events
    //Once in close range to player enemy will throw a punch 
    public void startPunch()
    {
        Debug.Log("AASS");
        animator.SetTrigger("isPunching");
        Debug.Log("AASSAASSAASS");
        agent.ResetPath();
        punchHitbox.enabled = true;
    }

    public void duringPunch()
    {
        punchHitbox.enabled = false;
    }

    public void finishPunch()
    {
        //animator.SetTrigger("isIdle");
        //animator.ResetTrigger("isPunching");
        stateMachine.changeState(new MeleeIdleState());
    }

    protected override void entityAttack()
    {
        startPunch();
    }

    protected override void entityDie()
    {
        destroyThis(transform.parent.gameObject);
    }

    //Determines if player is close enough to melee
    public void attackChecker()
    {
        Debug.Log("Check attack");
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= attackRange)
        {
            stateMachine.changeState(new MeleePunchState());
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Transform[0];
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (EntityHealth <= 0)
        {
            entityDie();
        }
        animator.SetFloat("velX", rb.velocity.x);
        animator.SetFloat("velZ", rb.velocity.z);
        agent.speed = EntitySpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
