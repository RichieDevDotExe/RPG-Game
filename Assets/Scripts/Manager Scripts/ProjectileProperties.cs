using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

/*
 
Author: Richard
Desc: Contains the base functions for all projectiles in the game
 
 */

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    protected Collider hitbox;
    protected Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Rigidbody RB
    {
        get { return rb; }
        set { rb = value; }
    }


    public virtual void spawnProjectile(GameObject selectedProjectile,Transform target)
    {
        //Debug.Log("Player pos 2 " + target.transform.position);
        //Debug.Log("Object 2 " + selectedProjectile.name);
        GameObject newProjectile = Instantiate(selectedProjectile);
        //Debug.Log("Object 2 " + newProjectile.name);
        newProjectile.GetComponent<Projectile>().launchProjectile(target);
    }

    public virtual void launchProjectile(Transform target)
    {
        //Debug.Log("HEY THIS IS TARGET "+target);
        //Debug.Log("HEY THIS IS POSITION " + transform.position);
        Vector3 forceCal = (target.transform.position - transform.position) * speed;
        rb.AddForce(forceCal, ForceMode.Impulse);
    }
    public virtual void projectileHit()
    {
        Debug.Log("EXPLOOOOOOOOOOOOOOOOOOOOOOSION :3 Daamge dealt" + damage);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("PlayerHIt");
            player.entityTakeDamage(damage);
        }
        projectileHit();
    }
}
