using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves when not aiming
    [SerializeField] float moveSpeed;
    // Speed at which the player rotates
    [SerializeField] float turnSpeed;
    // Visualize the cursor
    [SerializeField] GameObject cursor;

    // Reference to the CharacterController component
    CharacterController m_controller;
    // Reference to the Animator component
    Animator m_animator;
    // Reference to the main camera
    Camera m_camera;
    // Plane to handle mouse raycasting
    Plane m_plane;
    // Boolean to track whether the player is aiming
    bool m_isAiming;
    // Direction the player is moving in
    Vector3 m_moveDirection; 
    void Awake()
    {
        // Catch required components
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_camera = Camera.main;
    }

    void Start()
    {
        // Initialize the plane for raycasting at the world origin
        m_plane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimations();
    }

    void HandleMovement()
    {
        // Get movement input and normalize it to avoid faster diagonal movement
        m_moveDirection = InputManager.GetMovementInput().normalized;

        // Set the movement speed depending on whether the player is aiming
        float currentSpeed = m_isAiming ? 2.0f : moveSpeed;

        // Only move the character if there is movement input
        if (m_moveDirection.magnitude >= 0.1f)
        {
            // Move the character using the CharacterController
            m_controller.Move(m_moveDirection * currentSpeed * Time.deltaTime);
        }
    }

    void HandleRotation()
    {
        if (m_isAiming)
            // Rotate towards the mouse position if aiming
            RotateTowardsTheMouse();
        else
            // Rotate towards the movement direction if not aiming
            RotateTowardsMovementDirection();
    }

    void RotateTowardsMovementDirection()
    {
        // Only rotate the character if there is movement input
        if (m_moveDirection != Vector3.zero)
        {
            // Calculate the target rotation angle based on movement direction
            float targetAngle = Mathf.Atan2(m_moveDirection.x, m_moveDirection.z) * Mathf.Rad2Deg;

            // Create a new Quaternion rotation based on the target angle
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }

    void RotateTowardsTheMouse()
    {
        // Cast a ray from the camera to the mouse position on the plane
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (m_plane.Raycast(ray, out float distance))
        {
            // Calculate the point where the ray intersects the plane
            Vector3 hitPoint = ray.GetPoint(distance);

            // Get the direction from the player to the mouse position, ignoring the y-axis (height)
            Vector3 hitDirection = (hitPoint - transform.position).normalized;
            hitDirection.y = 0; // Ignore height difference

            // Only rotate if there's a significant difference in direction
            if (Vector3.Dot(transform.forward, hitDirection) < 1.0f)
            {
                // Calculate the rotation to face the mouse position
                Quaternion targetRotation = Quaternion.LookRotation(hitDirection);

                // Smoothly rotate the player towards the mouse position
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    void HandleAnimations()
    {
        // Get horizontal and vertical movement input relative to the player's forward and right directions
        float horizontal = Vector3.Dot(m_moveDirection, transform.right);
        float vertical = Vector3.Dot(m_moveDirection, transform.forward);

        // Set the animator parameters for movement direction
        m_animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        m_animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        m_animator.SetFloat("Magnitude", m_moveDirection.magnitude, 0.1f, Time.deltaTime);

        // Check if the player is aiming
        bool aiming = InputManager.IsAiming();

        // Only update the animator's aiming state if the aiming status has changed
        if (aiming != m_isAiming)
        {
            m_isAiming = aiming; // Update the aiming status
            m_animator.SetBool("Aiming", m_isAiming);
        }
    }
}
