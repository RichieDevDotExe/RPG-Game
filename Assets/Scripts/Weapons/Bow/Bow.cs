using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private Animator animator;
    private BoxCollider hitbox;
    void Start()
    {
        animator = Player.instance.GetComponent<Animator>();
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
        animator.SetBool("isBow", true);
    }
    public override void onUnEquip()
    {
        animator.SetBool("isBow", false);
    }
}