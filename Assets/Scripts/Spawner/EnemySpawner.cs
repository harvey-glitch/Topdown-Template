using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 1f;

    float m_nextspawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanSpawnAgain())
            SpawnEnemy();
    }

    bool CanSpawnAgain()
    {
        // USed to avoid spamming input for spawning
        return Time.time >= m_nextspawnTime;
    }

    void SpawnEnemy()
    {
        // Get the enemy prefab on the pool
        GameObject enemySpawned = ObjectPool.instance.GetObject("Enemy");

        // Set the enemy transform and rotation
        enemySpawned.transform.position = transform.position;
        enemySpawned.transform.rotation = transform.rotation;

        // Access the script and provoked the ai upon spawning
        // To make the ai chase the player immediately
        EnemyAI enemyAIScript = enemySpawned.GetComponent<EnemyAI>();
        enemyAIScript.isProvoked = true;

        // Set the time for next spawn
        m_nextspawnTime = Time.time + 1f / spawnInterval;
    }
}
