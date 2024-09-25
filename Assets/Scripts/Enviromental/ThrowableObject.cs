using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: Contains the functions for the ThrowableObject type of projectile
 
 */

public class ThrowableObject : Projectile
{

    public override void Awake()
    {
        
    }
    //override spawnprojectile function to apply ridgedbody to throwables
    public override void spawnProjectile(GameObject selectedProjectile, Transform target)
    {
        //Debug.Log("Player pos 2 " + target.transform.position);
        Debug.Log("Object 2 " + selectedProjectile.name);
        GameObject newProjectile = Instantiate(selectedProjectile, transform.position, transform.rotation);
        Debug.Log("Object 2 " + newProjectile.name);
        newProjectile.GetComponent<Projectile>().RB = newProjectile.AddComponent<Rigidbody>();
        newProjectile.GetComponent<Projectile>().launchProjectile(target);
    }

}
