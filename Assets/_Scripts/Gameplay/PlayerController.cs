using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int InJump = Animator.StringToHash("InJump");

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

    [Header("Animations")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private float _moveInput;
    private float _coyoteTimeCounter;
    private bool _inJump;
    private bool _controlsEnabled = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        InitTeleportPlayer();

        SetupCameraFollow();
    }

    public void EnableControls(bool enable)
    {
        _controlsEnabled = enable;
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
        _moveInput = Input.GetAxisRaw("Horizontal");
        SpriteAnimate();


        // Update coyote time
        if (_isGrounded)
            _coyoteTimeCounter = coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;

        // Jump
        if (_coyoteTimeCounter > 0f && Input.GetButtonDown("Jump") && _controlsEnabled)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }

        // Clamp upward speed
        if (_rb.linearVelocity.y > maxJumpVelocity)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, maxJumpVelocity);
        }

        // Better jump physics (arcade snappy style)
        if (_rb.linearVelocity.y > 0) // going up
        {
            // Почти нет замедления при удержании кнопки
            float gravityBoost = Input.GetButton("Jump") ? lowJumpMultiplier-0.5f : lowJumpMultiplier; 
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (gravityBoost - 1) * Time.deltaTime;
    
            // Резкий переход в падение
            if (_rb.linearVelocity.y < 2f) // near apex
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (apexGravityMultiplier) * Time.deltaTime;
            }
        }
        else if (_rb.linearVelocity.y < 0) // falling
        {
            // Быстрое падение
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }


        // Extra gravity near apex for snappy fall
        if (Mathf.Abs(_rb.linearVelocity.y) < 0.5f && !_isGrounded)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (apexGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    private void SpriteAnimate()
    {
        if (_rb.linearVelocity.y > 0 && _inJump == false) //start jump
        {
            _inJump = true;
            //Debug.Log($"Start Jump");
        }

        if (_inJump == true && CheckIfGrounded()) //stop jump
        {
            _inJump = false;
            //Debug.Log($"Stop Jump");
        }
        
        animator.SetBool(InJump, _inJump);
        animator.SetFloat(Speed, Mathf.Abs(_moveInput * moveSpeed));
        if (_moveInput != 0)
        {
            spriteRenderer.flipX = _moveInput < 0;
        }
    }

    void FixedUpdate()
    {
        // Move player
        if (_controlsEnabled)
        {
            _rb.linearVelocity = new Vector2(_moveInput * moveSpeed, _rb.linearVelocity.y);
        }
        

        // Ground check
        _isGrounded = CheckIfGrounded();
    }

    private Collider2D CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}
