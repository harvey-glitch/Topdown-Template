using UnityEngine;

public class DamageManager : MonoBehaviour
{
    // Amount of damage this object have
    [SerializeField] float damageAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a HealthManager component
        HealthManager health = other.GetComponent<HealthManager>();
        if (health != null)
            health.TakeDamage(damageAmount);
    }
}
