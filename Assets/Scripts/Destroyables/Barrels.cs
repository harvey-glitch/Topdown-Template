using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrels : HealthManager
{
    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            OnHealthDepleted();
        }
    }

    protected override void OnHealthDepleted()
    {
        // Retrieve particle from the pool and instantiate it
        GameObject explosion = ObjectPool.instance.GetObject("Explosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;

        gameObject.SetActive(false); // disable the gameobject
    }
}
