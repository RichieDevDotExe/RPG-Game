using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Hitbox")]
    [SerializeField] private GameObject rightHand;
    private BoxCollider hitbox;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private GameObject startingWeapon;
    private AudioClip swordSwingFX;
    //private Enemy enemy;
    private Animator animator;
    private GameObject equippedWeapon;
    private Weapon equippedWeaponScript;

    void Start()
    {
        hitbox = rightHand.GetComponentInChildren<BoxCollider>();
        swordSwingFX = Player.instance.swordSwingSFX;
        animator = GetComponent<Animator>();

        equipWeapon(startingWeapon);
    }

    public void equipWeapon(GameObject weapon)
    {
        equippedWeapon = weapon;
        equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();
    }

    //public void playerAttack()
    //{
    //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
    //    {
    //        Player.instance.EntitySpeed = 0;
    //        Debug.Log("attack");
    //        Collider[] hitEnemies = Physics.OverlapBox(hitbox.position + (Vector3.up * hitBoxOffsetY) + (Vector3.forward * hitBoxOffsetX), hitBoxSize, hitbox.rotation, enemyLayers);
    //        animator.SetTrigger("isAttacking");
    //        foreach (Collider targetEnemy in hitEnemies)
    //        {
    //            Debug.Log("Hit");
    //            enemy = targetEnemy.gameObject.GetComponent<Enemy>();
    //            Debug.Log(enemy.name);
    //            enemy.entityTakeDamage(Player.instance.EntityDamage);
    //        }
    //    }
    //}

    //sets animator to attack and plays attack sound

    public void playerAttack()
    {
        //animator.SetBool("isBow", true);
        equippedWeaponScript.attack();
        /*
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
        {
            animator.SetTrigger("isAttacking");
            //SoundFXManager.instance.playSoundEffect(swordSwingFX, transform, 1f);
        }
        */
    }
    //called in animator
    public void startAttackAnimation()
    {
        equippedWeaponScript.startAttackAnimation();
    }
 
    public void endAttackAnimation()
    {
        equippedWeaponScript.endAttackAnimation();
    }
    /*

    //checks if enemy hits collider and deals damage
    //Should be noted collider and script is found attached to the weapon the player prephab
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.entityTakeDamage(Player.instance.EntityDamage);
        }
    }
    */
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawCube(hitbox.position, hitBoxSize);
    //}
}