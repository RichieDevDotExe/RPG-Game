using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    // Start is called before the first frame update
    void Start()
    {

    }

    /* public override void spawnProjectile(GameObject selectedProjectile, Vector3 target, Transform spawnLoaction)
     {
         GameObject newProjectile = Instantiate(selectedProjectile, spawnLoaction.position, spawnLoaction.rotation);
         newProjectile.GetComponent<Projectile>().launchProjectile(target);
     }*/
    public override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            return;
        }
        var entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            Debug.Log("EntityHit");
            entity.entityTakeDamage(damage);
        }
        projectileHit();
    }
}
