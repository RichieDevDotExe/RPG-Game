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
        /*//find the exact hit position using a raycast
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //just a point far away from the player*/
        //}

        //calculate direction from attackPoint to targetPoint
        //Vector3 arrowDirection = targetPoint - attackPoint.position;
        //Vector3 arrowDirection = lookTarget;

        

        //add height to the position
        //Vector3 spawningPosition = new Vector3(attackPoint.position.x, (attackPoint.position.y + 0.2f), attackPoint.position.z);

        arrow.GetComponent<Arrow>().spawnProjectile(arrow, lookTarget, attackPoint);

        //Instantiate projectile
        //GameObject currentArrow = Instantiate(arrow, spawningPosition, Quaternion.identity);

        //this line caused issues with rotation
        //currentArrow.transform.forward = arrowDirection.normalized;

        //add forces to arrow
        /*Debug.Log(arrowForce);
        currentArrow.GetComponent<Rigidbody>().AddForce(arrowDirection.normalized * arrowForce, ForceMode.Impulse);*/
        //currentArrow.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        //apply arrow damage value to arrow
        //currentArrow.GetComponent<ArrowCollision>().arrowDamage = arrowDamage;

        //make it so there is a delay between shots

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