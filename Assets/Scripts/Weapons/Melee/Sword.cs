using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private BoxCollider hitbox;
    void Start()
    {
        initWeapon();
        hitbox = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void startAttack()
    {
        Debug.Log("Sword Attack");
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
        animator.SetBool("isSword", true);
        //equipSocket();
    }

    public override void onUnEquip()
    {
        Debug.Log("unequipped sword!");
        animator.SetBool("isSword", false);
        unEquipSocket();
    }

    //not used in the sword class
    public override void releaseAttack()
    {     
    }
}
