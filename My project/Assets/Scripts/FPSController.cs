using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Camera")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    public float upDownRange = 90f;

    private float verticalRotation = 0f;
    private Rigidbody rb;
    private bool isGrounded;

    Animator animator;
    private Vector3 currentMovementDirection = Vector3.forward;

    private bool isFpsMode = false; // Flag to track first-person mode
    private Vector3 originalCameraPosition; 
    private Quaternion originalCameraRotation; 

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = this.GetComponent<Animator>();

        // Move the camera inside the player's head (optional)
        //playerCamera.transform.localPosition = new Vector3(0.8f, 1.65f, -1.2f);

        // Store the original camera position and rotation
        originalCameraPosition = playerCamera.transform.localPosition; 
        originalCameraRotation = playerCamera.localRotation; 

    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();

        if (Input.GetMouseButtonDown(1)) // 1 corresponds to the right mouse button
        {
            ToggleFpsMode(!isFpsMode); // Toggle the mode
        }
    }

    private void HandleMovement()
    {
        float moveSideways = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera orientation
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate the desired move direction
        Vector3 moveDirection = (forward * moveForward + right * moveSideways).normalized;

        currentMovementDirection = moveDirection; 

        // Apply movement speed
        Vector3 targetVelocity = moveDirection * moveSpeed;
        targetVelocity.y = rb.velocity.y; // Preserve vertical velocity for jumping and gravity
        rb.velocity = targetVelocity;

        // Check for jumping
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (moveDirection != Vector3.zero)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }

    private void HandleMouseLook()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        else
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

            if(isFpsMode)
            {
                float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

                // Rotate the camera vertically
                verticalRotation += mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
                playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

            }

            // Rotate the player horizontally
            transform.Rotate(0f, mouseX, 0f);

        }
    }

    private void ToggleFpsMode(bool enable)
    {
        if (enable && !isFpsMode)
        {
            // Switch to first-person mode
            playerCamera.transform.localPosition = new Vector3(0.0f, 1.7f, 0.4f); // Move the camera to the character's head
            playerCamera.localRotation = Quaternion.identity; // Reset camera rotation

            isFpsMode = true;
        }
        else if (!enable && isFpsMode)
        {
            // Switch back to third-person mode
            playerCamera.transform.localPosition = originalCameraPosition; // Restore original position
            playerCamera.localRotation = originalCameraRotation; // Restore original rotation

            isFpsMode = false;
        }
    }

}
