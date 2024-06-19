using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected float attackCooldown;

    private Animator playerAnimator;
    void Awake()
    {
        playerAnimator = Player.instance.GetComponent<Animator>();
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    public Animator PlayerAnimator
    {
        get { return playerAnimator; }
        set { playerAnimator = value; }
    }
    public abstract void onEquip();
    public abstract void onUnEquip();
    public abstract void attack();
    public abstract void startAttackAnimation();
    public abstract void endAttackAnimation();
}
