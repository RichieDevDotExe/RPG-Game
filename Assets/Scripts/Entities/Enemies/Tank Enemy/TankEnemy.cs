using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    [Header("Tank Stats")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float chargeStrength;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        player = Player.instance;
    }

    public override void entityTakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    protected override void entityAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override void entityDie()
    {
        throw new System.NotImplementedException();
    }

}
