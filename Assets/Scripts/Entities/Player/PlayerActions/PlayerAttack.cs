using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Hitbox")]
    private BoxCollider hitbox;
    [SerializeField] private LayerMask enemyLayers;
    private AudioClip swordSwingFX;
    //private Enemy enemy;
    private Animator animator;

    void Update()
    {

    }

    void Start()
    {

        hitbox = GetComponent<BoxCollider>();
        swordSwingFX = Player.instance.swordSwingSFX;
        animator = GetComponent<Animator>();
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

    /*
    public void playerAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RightHand@Attack01"))
        {
            animator.SetTrigger("isAttacking");
            Debug.Log("Attack");
            SoundFXManager.instance.playSoundEffect(swordSwingFX, transform, 1f);
        }
    }

    //called in animator
    public void activateAttackHitBox()
    {
        if (Time.time - Player.instance.LastPlayerAttack >= Player.instance.AttackCooldown)
        {
            Player.instance.EntitySpeed = 0f;
            Player.instance.LastPlayerAttack = Time.time;
            hitbox.enabled = true;
        }
    }

    public void deactivateAttackHitBox()
    {
        hitbox.enabled = false;
        Player.instance.EntitySpeed = 7;
    }

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawCube(hitbox.position, hitBoxSize);
    //}
    */
}