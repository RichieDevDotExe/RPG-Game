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
    private GameObject equippedWeapon;
    private Weapon equippedWeaponScript;
    private GameObject[] weaponSlots;

    //TEMPORARY THING
    [SerializeField] private GameObject equippedBow;

    void Start()
    {
        hitbox = rightHand.GetComponentInChildren<BoxCollider>();
        swordSwingFX = Player.instance.swordSwingSFX;

   
        weaponSlots = new GameObject[] {startingWeapon, equippedBow};

    }

    public void equipWeapon(GameObject weapon)
    {
        //only runs if a weapon is already equipped
        if (equippedWeapon)
        {
            equippedWeaponScript.onUnEquip();
        }

        equippedWeapon = weapon;
        equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();

        equippedWeaponScript.onEquip();
    }

    public void switchWeaponSlot(int slot)
    {
        //will equip the weapon in the specified slot if there is one
        if(weaponSlots.Length > slot)
        {
            equipWeapon(weaponSlots[slot]);
        }
        //temporary for debugging
        else
        {
            Debug.Log("No weapon in slot" + slot);
        }
        
    }

    //sets animator to attack and plays attack sound
    public void playerAttack()
    {
        if (equippedWeapon)
        {
            equippedWeaponScript.attack();
        }
        //just for debugging
        else
        {
            Debug.Log("No Weapon Equipped!");
        }
        //animator.SetBool("isBow", true);
        
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

    //when this is called it will put the equipped weapon into it's hand socket
    public void socketEquippedWeapon()
    {
        equippedWeaponScript.equipSocket();
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