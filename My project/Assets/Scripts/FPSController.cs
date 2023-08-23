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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Move the camera inside the player's head (optional)
        playerCamera.transform.localPosition = new Vector3(0f, 0.8f, 0f);
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
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
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(0f, mouseX, 0f);

        // Rotate the camera vertically
        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
