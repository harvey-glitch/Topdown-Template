using UnityEngine;

public class EnemyHealth : HealthManager
{
    // Reference to the enemy controller class
    EnemyAI m_enemyAI;

    void Awake()
    {
        // Catach required components
        m_enemyAI = GetComponent<EnemyAI>();
    }

    public override void TakeDamage(float damageAmount)
    {
        // Call the base class method to reduce health
        base.TakeDamage(damageAmount);

        // Only provoked the enemy once
        if (!m_enemyAI.isProvoked)
        {
            m_enemyAI.OnProvoked();
        }
    }

    protected override void OnHealthDepleted()
    {
        // Get the death particle from the pool
        GameObject deathParticle = ObjectPool.instance.GetObject("Death");

        // Set the particle's position and rotation
        deathParticle.transform.position = transform.position;
        deathParticle.transform.rotation = Quaternion.identity;

        // Disable the gameobject
        gameObject.SetActive(false);
    }
}
