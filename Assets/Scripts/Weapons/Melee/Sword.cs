using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private BoxCollider hitbox;
    private Animator animator;
    void Start()
    {
        Debug.Log("ayoooooooo");
        hitbox = GetComponentInChildren<BoxCollider>();
        animator = Player.instance.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void attack()
    {
        Debug.Log(animator);
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

    public override void onEquip()
    {
        Debug.Log("equipped sword!");
    }

    public override void onUnEquip()
    {
        Debug.Log("unequipped sword!");
    }
}
