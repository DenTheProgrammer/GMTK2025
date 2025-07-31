using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Better Jump Settings")]
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 3f; 
    public float coyoteTime = 0.15f;
    public float maxJumpVelocity = 7f; // limit upward speed
    public float apexGravityMultiplier = 2f; // extra gravity near jump apex

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private float coyoteTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        InitTeleportPlayer();

        SetupCameraFollow();
    }

    private void SetupCameraFollow()
    {
        CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
        cameraFollow.target = transform;
    }

    private void InitTeleportPlayer()
    {
        PlayerSpawnPoint spawnPoint = FindAnyObjectByType<PlayerSpawnPoint>();
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    void Update()
    {
        // Horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Update coyote time
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump
        if (coyoteTimeCounter > 0f && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Clamp upward speed
        if (rb.linearVelocity.y > maxJumpVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpVelocity);
        }

        // Better jump physics
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Extra gravity near apex for snappy fall
        if (Mathf.Abs(rb.linearVelocity.y) < 0.5f && !isGrounded)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (apexGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Move player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}
