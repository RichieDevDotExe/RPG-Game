using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected float attackCooldown;

    public Animator animator;
    protected void initWeapon()
    {
        animator = Player.instance.PlayerAnimator;
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    public abstract void onEquip();
    public abstract void onUnEquip();
    public abstract void attack();
    public abstract void startAttackAnimation();
    public abstract void endAttackAnimation();
}
