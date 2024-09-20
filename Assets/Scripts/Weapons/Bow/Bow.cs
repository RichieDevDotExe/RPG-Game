using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private BoxCollider hitbox;
    void Start()
    {
        initWeapon();
        //hitbox = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void attack()
    {
        Debug.Log("Bow Attack!");

    }
    public override void startAttackAnimation()
    {
        
    }
    public override void endAttackAnimation()
    {
        
    }

    public override void onEquip()
    {
        Debug.Log("equipped bow!");
        animator.SetBool("isBow", true);
    }
    public override void onUnEquip()
    {
        Debug.Log("unequipped bow!");
        animator.SetBool("isBow", false);
    }
}