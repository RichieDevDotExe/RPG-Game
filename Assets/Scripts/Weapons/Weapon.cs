using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected float attackCooldown;

    public Animator animator;
    public MeshSockets.SocketID handSocket;
    public MeshSockets.SocketID backSocket;
    public MeshSockets sockets;
    protected Vector3 lookTarget;
    protected void initWeapon()
    {
        animator = Player.instance.PlayerAnimator;
        sockets = Player.instance.Sockets;
        lookTarget = Player.instance.LookTarget;
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    public abstract void onEquip();
    public abstract void onUnEquip();
    public void equipSocket()
    {
        //equip weapon to weapon socket
        sockets.Attach(transform, handSocket);
    }
    public void unEquipSocket()
    {
        sockets.Attach(transform, backSocket);
    }
    public abstract void startAttack();
    public abstract void releaseAttack();
    public abstract void startAttackAnimation();
    public abstract void endAttackAnimation();
}
