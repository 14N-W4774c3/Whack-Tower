using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // NOTE: REDO PLAYER MOVEMENT
    [Header("Player Movement")]
    [Tooltip("Controls the speed of player movement.")]
    [SerializeField] private float moveSpeed = 8f;    // Speed of the player movement
    [Tooltip("Controls the force of player jumps.")]
    [SerializeField] private float jumpForce = 12f;    // Force applied when the player jumps

    private Rigidbody rb;            // Reference to the Rigidbody component
    private bool isGrounded;         // Tracks whether the player is on the ground

    private void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();                      // Handle player movement
        HandleJump();               // Check for jump input and apply jump if conditions are met
    }

    private void Move()
    {
        // Get input from horizontal and vertical axes (WASD or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement direction vector based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player if there is any input
        if (moveDirection.magnitude >= 0.1f)
        {
            // Calculate the movement vector in the player's facing direction
            Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveVector);
        }
    }

    private void HandleJump()
    {
        // Check if the player is on the ground and the jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force to the Rigidbody to make the player jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Set grounded to false as the player is now in the air
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set grounded to true to allow jumping again
        }
    }
}
