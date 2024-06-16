using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    [Header("Tank Stats")]
    
    [SerializeField] private float maxSpeed;
    [SerializeField] private float chargeStrength;
    private Rigidbody rb;
    TankValues tankValues = (TankValues)enemyValues;
    private float saveSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody>(); 
        player = Player.instance;
    }

    public override void entityTakeDamage(float damage)
    {
        health -= damage;
    }

    public override void Init()
    {
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
        stateMachine.Init();

        //include code to get enemy path
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void entityAttack()
    {
        throw new System.NotImplementedException();
    }
    public override void enemyAttack()
    {
        entityAttack();
    }

    protected override void entityDie()
    {
        destroyThis(transform.parent.gameObject);
    }

    public void startCharge()
    {
        saveSpeed = speed;
        agent.ResetPath();
        speed = 0;
        //play start sound
    }
    public void performCharge()
    {
        //play charge sound
        animator.speed = 3;
        hitbox.enabled = true;
        Vector3 forceCal = (player.transform.position - transform.position) * chargeStrength;
        
        if (forceCal.magnitude > maxSpeed)
        {
            var direction = forceCal.normalized;
            forceCal = direction * maxSpeed;
        }
        rb.AddForce(forceCal, ForceMode.Impulse);
        Vector3 targetDirection = player.transform.position;
        transform.LookAt(targetDirection);
    }

    public void endCharge()
    {
        animator.speed = 1;
        hitbox.enabled = false;
        speed = saveSpeed;
        rb.velocity = Vector3.zero;
        stateMachine.changeState(new TankIdleState());
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
