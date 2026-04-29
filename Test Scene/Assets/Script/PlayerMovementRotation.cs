using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController characterController;
    public InputActionAsset inputActions;
    public float mouseSensitivity = 100f;
    private Camera playerCamera;
    private float xRotation = 0f;
    public bool canMove = true;

    [Header("Gravity Settings")] [SerializeField]
    private float gravity = -9.81f;

    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private bool debugGravityRaycast = true;

    // Path movement setup
    public bool usePathMovement = false;
    public List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypointIndex = 0;
    public float waypointTolerance = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on " + gameObject.name);
        }

        playerCamera = GetComponentInChildren<Camera>();

        if (inputActions != null)
        {
            inputActions.Enable();
        }
        else
        {
            Debug.LogError("InputActionAsset not assigned in " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (!canMove) return;

        if (usePathMovement)
        {
            FollowPath();
        }
        else
        {
            HandleInputMovement();
        }

        ApplyGravity();

        if (playerCamera != null)
        {
            HandleMouseLook();
        }
    }

    private void ApplyGravity()
    {
        if (characterController == null) return;

        // Calculate the bottom center of the collider
        Vector3 bottom = transform.position +
                         Vector3.down * (characterController.height / 2f - characterController.radius);

        // Check if player is grounded by casting a ray downward from the bottom of the collider
        bool isGrounded = Physics.Raycast(bottom, Vector3.down, groundCheckDistance);

        // Draw debug raycast
        if (debugGravityRaycast)
        {
            Color rayColor = isGrounded ? Color.green : Color.red;
            Debug.DrawRay(bottom, Vector3.down * groundCheckDistance, rayColor);
        }

        if (!isGrounded)
        {
            // Apply gravity if not grounded
            Vector3 gravityVector = new Vector3(0, gravity * Time.deltaTime, 0);
            characterController.Move(gravityVector);
        }
    }

    private void HandleInputMovement()
    {
        if (inputActions == null) return;

        var moveAction = inputActions.FindAction("Move");
        if (moveAction == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime;

        characterController.Move(movement);
    }

    private void HandleMouseLook()
    {
        if (inputActions == null) return;

        var lookAction = inputActions.FindAction("Look");
        if (lookAction == null) return;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotate the player object horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FollowPath()
    {
        if (waypoints.Count == 0) return;

        Vector3 target = waypoints[currentWaypointIndex];
        Vector3 direction = (target - transform.position).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        characterController.Move(movement);

        if (Vector3.Distance(transform.position, target) < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}