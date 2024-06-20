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
    private GameObject[] weaponSlots;

    public MeshSockets sockets;

    //TEMPORARY THING
    [SerializeField] private GameObject equippedBow;

    void Start()
    {
        hitbox = rightHand.GetComponentInChildren<BoxCollider>();
        swordSwingFX = Player.instance.swordSwingSFX;
        animator = GetComponent<Animator>();

   
        weaponSlots = new GameObject[] {startingWeapon, equippedBow};
        Debug.Log(weaponSlots[1]);

        equipWeapon(startingWeapon);

        //weapon sockets
        sockets = GetComponent<MeshSockets>();
    }

    public void equipWeapon(GameObject weapon)
    {
        //only runs if a weapon is already equipped
        if (equippedWeapon)
        {
            equippedWeaponScript.onUnEquip();
        }

        //equip weapon to weapon socket
        sockets.Attach(weapon.transform, MeshSockets.SocketID.Spine);

        equippedWeapon = weapon;
        equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();

        equippedWeaponScript.onEquip();
    }

    public void switchWeaponSlot(int slot)
    {
        Debug.Log("helo");
        Debug.Log(weaponSlots[1]);
        equipWeapon(weaponSlots[slot]);
    }

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