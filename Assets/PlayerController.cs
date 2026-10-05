using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float Speed;
    [SerializeField]
    private float SprintSpeed;
    [SerializeField]
    private float Accelerate;
    [SerializeField]
    private float Decelerate;
    [SerializeField]
    private float JumpVelocity;
    [SerializeField]
    private Transform GroundCheck;
    [SerializeField]
    private float GroundCheckWidth;
    [SerializeField]
    private float GroundCheckHeight;
    [SerializeField]
    private LayerMask GroundMask;
    private bool CanDoubleJump = false;
    private bool wasGrounded = false;
    private int RemainingJumps = 1;
    private Rigidbody2D rb;
    private float horizontalInput;
    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();

        inputActions.Player.Jump.performed -= OnJump;
    }

    void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (RemainingJumps > 0)
        {
            rb.linearVelocityY = JumpVelocity;
            RemainingJumps--;
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = inputActions.Player.Move.ReadValue<float>();
    }
    void FixedUpdate()
    {
        movePlayer();
        RunGroundCheck();
    }
    void movePlayer()
    {
        float targetSpeed = horizontalInput * Speed;

        float rate = MathF.Abs(targetSpeed) < 0.2f ? Decelerate : Accelerate;

        rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, targetSpeed, rate * Time.fixedDeltaTime);
    }
    private void RunGroundCheck()
    {
        bool grounded = Physics2D.OverlapBox(
        GroundCheck.position,
        new Vector2(GroundCheckWidth, GroundCheckHeight),
        0f,
        GroundMask
    );
        if (grounded && !wasGrounded)
        {
            RemainingJumps = CanDoubleJump ? 2 : 1;
        }
        wasGrounded = grounded;
    }

    public void GiveDoubleJump()
    {
        CanDoubleJump = true;
    }
    void OnDrawGizmos()
    {
        if (RemainingJumps == 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GroundCheck.position, new Vector3(GroundCheckWidth, GroundCheckHeight));
        }
    }
}
