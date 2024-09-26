using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
/*
 
Author: Richard
Desc: Contains the stats for the Tank enemy and functions for various Tank actions
 
 */
public class TankEnemy : Enemy
{
    [Header("Tank Stats")]
    //range for tank to detect throwables
    [SerializeField] private float pickUpRange;
    //range for tank to detect player to throw throwables at
    [SerializeField] private float throwDetectionRange;
    //lifetime of projectile
    [SerializeField] private float throwDistance;

    //max speed used to cap charge speed
    [SerializeField] private float maxSpeed;
    //range for tank to charge at player
    [SerializeField] private float chargeRange;
    //used to regulate how fast the tank can charge
    [SerializeField] private float chargeStrength;

    //debug to check what object the tank is throwing
    [SerializeField] private GameObject throwable;

    //Socket where the held throwable is on tank model
    private Transform attachPoint;
    private LayerMask throwableLayer;
    private Collider[] throwableHitBox;

    //attack to be passed into enemy attack function
    private Action currentAttack;
    private Rigidbody rb;
    private float saveSpeed;
    
    //tank stats
    private TankValues tankValues;

    [Header("Tank Attack Hitboxes")]
    [SerializeField] private Collider chargeHitbox;
    [SerializeField] private Collider punchHitbox;

    public float ChargeRange { get => chargeRange; }

    private void Start()
    {
        player = Player.instance;
    }

    private void Awake()
    {
        waypoints = new Transform[0];
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody>();
        attachPoint = transform.Find("ThrowableSocket");
        throwableLayer = 1 << 7;
        Init();
    }

    private void Update()
    {
        if (EntityHealth <= 0)
        {
            entityDie();
        }
        animator.SetFloat("velX", rb.velocity.x);
        animator.SetFloat("velZ", rb.velocity.z);
        agent.speed = EntitySpeed;

        
    }

    public override void entityTakeDamage(float damage)
    {
        health -= damage;
    }

    public override void Init()
    {
        tankValues = (TankValues)enemyValues;
        Debug.Log("Init" + name);
        maxHealth = tankValues.maxHealth;
        health = maxHealth;
        speed = tankValues.speed;
        damage = tankValues.damage;
        detectionRange = tankValues.detectionRange;
        fieldOfView = tankValues.fieldOfView;
        attackRange = tankValues.attackRange;
        maxSpeed = tankValues.maxSpeed;
        chargeStrength = tankValues.chargeStrength;
        chargeRange = tankValues.chargeRange;
        pickUpRange = tankValues.pickUpRange;
        throwDetectionRange = tankValues.throwDetectionRange;
        throwDistance = tankValues.throwDistance;
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
        player = Player.instance;
    }

    //creates waypoint list for enemy to follow
    public override void AddDescendantsWithTag(Transform parent, string tag)
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
    
    protected override void entityAttack()
    {
        Debug.Log("Trigger");
        currentAttack();
    }

    //passes in the function for selected attack and then executes it in entity attack
    public override void enemyAttack(Action attackCalled)
    {
        currentAttack = attackCalled;
        entityAttack();
    }

    protected override void entityDie()
    {
        destroyThis(transform.parent.gameObject);
    }

    //Functions used during the punch state of enemy. duringPunch() and finishPunch() run using animation events
    //Once in close range to player enemy will throw a punch 
    public void startPunch()
    {
        animator.SetBool("isPunching",true);
        agent.ResetPath();
        punchHitbox.enabled = true;
    }

    public void duringPunch()
    {
        punchHitbox.enabled = false;
    }

    public void finishPunch()
    {
        animator.SetTrigger("isIdle");
        animator.SetBool("isPunching", false);
        stateMachine.changeState(new TankIdleState());
    }

    //Functions used during the charge state of enemy. performCharge(), endCharge() and finishCharge() run using animation events
    //Once in range of player enemy will stop moving and ready a charge. Once charge is ready lunge at player. 
    public void startCharge()
    {
        Debug.Log("Charging");
        animator.SetTrigger("isCharging");
        saveSpeed = speed;
        agent.ResetPath();
        speed = 0;
        transform.LookAt(player.transform.position);
        //play start sound
    }
    public void performCharge()
    {
        //play charge sound
        Debug.Log("Lunge");
        animator.SetTrigger("isAttacking");
        animator.speed = 3;
        chargeHitbox.enabled = true;
        //calculates force to apply to tank to charge towards player
        Vector3 forceCal = (player.transform.position - transform.position) * chargeStrength;
        Debug.Log("cal = " + forceCal + " " + forceCal.magnitude);
        
        //caps how far enemy can charge
        if (forceCal.magnitude > maxSpeed)
        {
            var direction = forceCal.normalized;
            forceCal = direction * maxSpeed;
        }
        Debug.Log("norm = " + forceCal + " " + forceCal.magnitude);
        rb.AddForce(forceCal, ForceMode.Impulse);
        Vector3 targetDirection = player.transform.position;
        transform.LookAt(targetDirection);
    }


    public void endCharge()
    {
        Debug.Log("Done lunge");
        chargeHitbox.enabled = false;
        rb.velocity = Vector3.zero;
        stateMachine.changeState(new TankIdleState());
    }

    public void finishedCharge()
    {
        Debug.Log("Resetting");
        speed = saveSpeed;
        animator.SetTrigger("isIdle");
        animator.speed = 1;
        stateMachine.changeState(new TankIdleState());

    }

    //checks if throwable is in range whilst chasing player
    public GameObject checkForThrowable()
    {
        throwableHitBox = Physics.OverlapSphere(transform.position + (Vector3.up * 0), pickUpRange, throwableLayer);
        if ((throwable == null) && (throwableHitBox.Length != 0))
        {
            return throwableHitBox[0].gameObject;
        }
        return null;
    }

    //Functions used during the pickup state of enemy. equipThrowable() run using animation events
    //If enemy is chasing player and comes across a throwable object it will stop and take the time to pick up the throwable
    public void pickupThrowable(GameObject throwableSelected)
    {
        saveSpeed = speed;
        throwable = throwableSelected;  
        animator.SetTrigger("isPickingUp");
        speed = 0;
        Debug.Log("picking up");
    }

    public void equipThrowable()
    {
        throwable.transform.SetParent(attachPoint, false);
        throwable.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log("equip " + throwable.name);
        Debug.Log("Parent " + throwable.transform.parent.name);
        Debug.Log(saveSpeed);
        speed = saveSpeed;
        stateMachine.changeState(new TankIdleState());

    }

    //Functions used during the throwing state of enemy. throwThrowable() and endThrowable() run using animation events
    //If enemy is chasing player and is holding a throwable. once in range it will throw throwable at player
    public void startThrow()
    {
        saveSpeed = speed;
        speed = 0;
        animator.SetTrigger("isThrowing");
        Debug.Log("start throw");
    }

    public void throwThrowable()
    {
        Debug.Log("throwing");
        //Debug.Log("Player pos "+ player.transform.position);
        //Debug.Log("Object " + throwable.name);
        
        //Throwable projectile uses projectile system 
        throwable.GetComponent<ThrowableObject>().spawnProjectile(throwable,player.transform.position, throwable.transform);
        //throwable = null;
        Destroy(throwable);
    }

    public void endThrowable()
    {
        Debug.Log("stop throwing");
        //animator.SetTrigger("isIdle");
        speed = saveSpeed;
        stateMachine.changeState(new TankChaseState());

    }

    //Determines what attack state the enemy needs to be set to depending on distance between enemy and player as well as if enemy has a throwable
    public void attackChecker()
    {
        Debug.Log("Chech attack");
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= attackRange)
        {
            stateMachine.changeState(new TankMeleeState());
        }
        else if (distance <= chargeRange)
        {
            stateMachine.changeState(new TankChargeState());
        }
        else if ((checkForThrowable() != null) && (throwable == null))
        {
            stateMachine.changeState(new TankPickupState());
        }
        else if((throwDetectionRange >= distance) && (throwable != null)) 
        {
            stateMachine.changeState(new TankThrowState());
        }
    }

    //various ranges for enemy
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, throwDetectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("PlayerHIt");
            player.entityTakeDamage(damage);
        }
    }
}
