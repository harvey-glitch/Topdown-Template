using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Speed at which the projectile moves
    [SerializeField] float speed;
    // Speed at which the projectile moves
    [SerializeField] float damage;
    // Trail effect for the projectile
    [SerializeField] TrailRenderer trail;

    // Tracks the distance the projectile has traveled
    float m_distanceTraveled;

    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        // Move the projectile in the forward direction, based on its speed
        transform.position += transform.forward * speed * Time.deltaTime;

        // Accumulate the distance the projectile has traveled
        m_distanceTraveled += speed * Time.deltaTime;

        // Check if the projectile has traveled beyond the maximum allowed distance
        if (m_distanceTraveled >= 10)
        {
            // Call the method to reset the projectile data
            ResetProjectleData();

            // Call the method to return the projectile to the pool
            ReturnToPool();
        }
    }

    void ResetProjectleData()
    {
        // Reset the distance counter
        m_distanceTraveled = 0;

        // Clear the trail renderer to avoid visual bugs
        trail.Clear();
    }

    void ReturnToPool()
    {
        // Return the current projectile to the object pool
        ObjectPool.instance.ReturnObject("Projectile", gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the projectile collided with has the "Interactable" tag
        if (other.transform.CompareTag("Interactable"))
        {
            // Retrieve an impact particle effect from the pool
            GameObject impactParticle = ObjectPool.instance.GetObject("Impact");

            // Set the impact particle's position and rotation to match the projectile's transform
            impactParticle.transform.position = transform.position;
            impactParticle.transform.rotation = Quaternion.identity;

            // Call the method to reset the projectile data
            ResetProjectleData();

            // Call the method to return the projectile to the pool
            ReturnToPool();
        }
    }
}

