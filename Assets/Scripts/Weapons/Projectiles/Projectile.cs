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


    public virtual void spawnProjectile(GameObject selectedProjectile,Vector3 target, Transform spawnLoaction)
    {
        GameObject newProjectile = Instantiate(selectedProjectile, spawnLoaction.position, spawnLoaction.rotation);
        newProjectile.GetComponent<Projectile>().launchProjectile(target);
    }

    public virtual void launchProjectile(Vector3 target)
    {
        //Debug.Log("HEY THIS IS TARGET "+target);
        //Debug.Log("HEY THIS IS POSITION " + transform.position);
        Vector3 forceCal = (target - transform.position) * speed;
        rb.AddForce(forceCal, ForceMode.Impulse);
    }
    public virtual void projectileHit()
    {
        Debug.Log("EXPLOOOOOOOOOOOOOOOOOOOOOOSION :3 Daamge dealt" + damage);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            Debug.Log("EntityHit");
            entity.entityTakeDamage(damage);
        }
        projectileHit();
    }
}
