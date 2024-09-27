using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Author: Richard
Desc: not in use ATM
 
 */

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void shootProjectile(GameObject selectedProjectile,Transform target)
    {
        Projectile projectileProperties = selectedProjectile.GetComponent<Projectile>();
        if (projectileProperties != null)
        {
            projectileProperties.spawnProjectile(selectedProjectile, target.position, selectedProjectile.transform);
        }
    }
}
