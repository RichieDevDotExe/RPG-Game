using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private BoxCollider hitbox;

    public GameObject arrow;
    //public AudioSource chargedSound;
    public float rangedAttackDelay = 0.4f;
    public float rangedAttackSpeed = 1f;
    public float maxArrowDamage = 5f;
    public float minArrowDamage = 1f;
    public float maxArrowForce = 2f;
    public float minArrowForce = 0.5f;
    public float upwardForce = 2f;
    public Transform attackPoint;
    public float chargeTime = 1f;
    private bool charging = false;
    private float bowTimer = 0f;
    private float arrowDamage;
    private float arrowForce;
    void Start()
    {
        initWeapon();
        //hitbox = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        //check if the bow is currently charging
        if (charging)
        {
            bowTimer += Time.deltaTime;
            //returns the a percentage of the max arrow damage based on the time charged or the min arrow damage if it is greater
            arrowDamage = Mathf.Max(maxArrowDamage / (chargeTime / bowTimer), minArrowDamage);

            //returns the a percentage of the max arrow force based on the time charged or the min arrow force if it is greater
            arrowForce = Mathf.Max(maxArrowForce / (chargeTime / bowTimer), minArrowForce);
            if (bowTimer >= chargeTime)
            {
                charging = false;
                arrowDamage = maxArrowDamage;
                arrowForce = maxArrowForce;
                //activate charged effects (sound effect)
                //chargedSound.Play();
            }
        }
    }

    public override void startAttack()
    {
        Debug.Log("Bow Attack!");

        charging = true;
        bowTimer = 0f;
    }
    public override void releaseAttack()
    {

        arrow.GetComponent<Arrow>().spawnProjectile(arrow, Player.instance.LookTarget, attackPoint);

        //this line caused issues with rotation
        //currentArrow.transform.forward = arrowDirection.normalized;

        
        //currentArrow.GetComponent<Rigidbody>().AddForce(arrowDirection.normalized * arrowForce, ForceMode.Impulse);


        //apply arrow damage value to arrow
        //currentArrow.GetComponent<ArrowCollision>().arrowDamage = arrowDamage;


        charging = false;


        //play random release sound effect
        //arrowReleaseSounds.PlayRandomSound();

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
        unEquipSocket();
    }
}