using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Flag to indicate if the AI is provoked or not
    [HideInInspector] public bool isProvoked;
    // Reference to the transform component of the target / player
    [SerializeField] Transform target;
    // The distance at which the AI can detect the target.
    [SerializeField] float detectionRange= 5.0f;
    // The distance within which the AI will start attacking the target.
    [SerializeField] float attackDistance = 2.0f;
    // A layer mask to define obstacles that could block the AI's line of sight.
    [SerializeField] LayerMask obstacleMask;

    // Reference to the NavMeshAgent component
    NavMeshAgent m_agent;
    // Reference to the Animator component
    Animator m_animator;
    // Flag to indicate whether the AI is currently in the middle of an attack.
    bool m_isAttacking;
    // Stores the last known position of the target
    Vector3 m_targetLastPosition;
    // The next time when the AI should update its chase destination.
    float m_nextUpdateTime;

    // Enumeration for different AI states: idle, chasing, and attacking.
    enum States
    {
        idle, 
        chase,
        attack
    }

    // The current state of the AI
    [SerializeField] States currentState;

    void Awake()
    {
        // Catch required components
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Set the state to idle and disable the AI's movement
        ChangeState(States.idle, true);

        // Disable the ai rotation (to be controlled via script)
        m_agent.updateRotation = false;
    }

    void Update()
    {
        StateTransition();
    }

    void StateTransition()
    {
        switch (currentState)
        {
            case States.idle:
                SearchForTarget();
                break;
            case States.chase:
                ChaseTheTarget();
                HandleAnimations();

                if (GetTargetDistance() <= attackDistance)
                    ChangeState(States.attack, true);
                break;
            case States.attack:
                AttackTheTarget();
                break;
        }

        LookAtTheTarget();
    }

    void SearchForTarget()
    {
        // Check if the target is within the detection distance
        if (GetTargetDistance() <= detectionRange)
        {
            // Check if the target is visible
            if (IsTargetObstructed())
            OnProvoked();
        }
    }

    void ChaseTheTarget()
    {
        // Update the destination based on interval
        if (Time.time >= m_nextUpdateTime)
        {
            // Check if the player has moved significantly since the last update
            if (Vector3.Distance(m_targetLastPosition, target.position) > 1.0f)
            {
                UpdateDestination();
            }
            // schedule the next update based on the update interval
            m_nextUpdateTime = Time.time + 0.30f; // Adjust as needed
        }
    }

    void AttackTheTarget()
    {
        // If hasn't attack yet, attack
        if (!m_isAttacking)
            StartCoroutine(PerformAttack());
    }

    void LookAtTheTarget()
    {
        // Only starts rotating when provoked
        if (!isProvoked) return;

        // Get the direction from the AI to the target
        Vector3 directionToTarget = GetTargetDirection();
        directionToTarget.y = 0; // Ignore height difference

        // Check if the rotation difference is significant
        float directionDifference = Vector3.Dot(transform.forward.normalized, directionToTarget.normalized);

        // Adjust for threhold (1.0 means perfectly aligned)
        if (directionDifference < 0.99f)
        {
            // Rotate the enemy to face the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        }
    }

    void HandleAnimations()
    {
        // Set the Speed parameter using agent's speed in the blend tree
        m_animator.SetFloat("Speed", m_agent.velocity.magnitude);
    }

    void UpdateDestination()
    {
        // Update the player's last known position
        m_targetLastPosition = target.position;

        // Update the enemy destination
        m_agent.SetDestination(target.position);
    }

    void ChangeState(States newState, bool stopMovement)
    {
        currentState = newState;
        m_agent.isStopped = stopMovement;
    }

    // Set to public to be accessible by healh script
    public void OnProvoked()
    {
        isProvoked = true;
        ChangeState(States.chase, false);
        UpdateDestination();
    }

    IEnumerator PerformAttack()
    {
        // Mark as attacking
        m_isAttacking = true;

        // Continuously check distance to target while within attack range
        while (GetTargetDistance() <= attackDistance)
        {
            // Trigger attack animation
            m_animator.SetTrigger("Attack");

            // Wait for the animation to finish
            yield return new WaitForSeconds(1.30f); // Adjust as needed

            // If the target is out of range, transition to the chase state.
            if (GetTargetDistance() > attackDistance)
            {
                ChangeState(States.chase, false);
                break;
            }
        }
        // Reset attack flag
        m_isAttacking = false;
    }

    #region UTILITY METHODS
    float GetTargetDistance()
    {
        // return the distance between the AI and target
        return Vector3.Distance(transform.position, target.position);
    }

    Vector3 GetTargetDirection()
    {
        // return the direction from the AI to the target
        return (target.position - transform.position).normalized;
    }

    bool IsTargetObstructed()
    {
        // Check if theres an obstacle block the player
        return !Physics.Raycast(transform.position, GetTargetDirection(), GetTargetDistance(), obstacleMask);
    }
    #endregion
}
