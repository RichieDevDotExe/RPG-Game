using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Animator animator;
    private BoxCollider hitbox;
    void Start()
    {
        animator = Player.instance.GetComponent<Animator>();
        hitbox = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
        {
            animator.SetTrigger("isAttacking");
            //SoundFXManager.instance.playSoundEffect(swordSwingFX, transform, 1f);
        }

    }
    public override void startAttackAnimation()
    {
        activateAttackHitBox();
    }
    public override void endAttackAnimation()
    {
        deactivateAttackHitBox();
    }


    public void activateAttackHitBox()
    {
        if (Time.time - Player.instance.LastPlayerAttack >= Player.instance.AttackCooldown)
        {
            //Player.instance.EntitySpeed = 0f;
            Player.instance.LastPlayerAttack = Time.time;
            hitbox.enabled = true;
        }
    }

    public void deactivateAttackHitBox()
    {
        hitbox.enabled = false;
        //Player.instance.EntitySpeed = 7;
    }
}
