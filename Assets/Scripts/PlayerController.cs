using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float NormalSpeed;
    private float Speed;
    [SerializeField]
    private float SprintSpeed;
    [SerializeField]
    private float SprintStaminaDrainRate;
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
    [SerializeField]
    private float Stamina;
    [SerializeField]
    private float StaminaMax;
    [SerializeField]
    private float PassiveStaminaGainRate;
    [SerializeField]
    private Slider StaminaBar;
    private bool CanDoubleJump = false;
    private bool WasGrounded = false;
    private bool IsSprinting = false;
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
        inputActions.Player.Sprint.started += EnterSprint;
        inputActions.Player.Sprint.canceled += ExitSprint;

    }

    void OnDisable()
    {
        inputActions.Player.Disable();

        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Sprint.started -= EnterSprint;
        inputActions.Player.Sprint.canceled -= ExitSprint;
    }

    void EnterSprint(InputAction.CallbackContext callbackContext)
    {
        IsSprinting = true;
    }
    void ExitSprint(InputAction.CallbackContext callbackContext)
    {
        IsSprinting = false;
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
        Speed = NormalSpeed;
        StaminaBar.interactable = false;
    }

    void Update()
    {
        horizontalInput = inputActions.Player.Move.ReadValue<float>();
        StaminaControl();

        Debug.Log(Speed);
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
        if (grounded && !WasGrounded)
        {
            RemainingJumps = CanDoubleJump ? 2 : 1;
        }
        WasGrounded = grounded;
    }

    public void GiveDoubleJump()
    {
        CanDoubleJump = true;
    }

    void StaminaControl()
    {
        if (IsSprinting && horizontalInput != 0)
        {
            Stamina -= Time.deltaTime * SprintStaminaDrainRate;
        }
        if (Stamina < 0)
        {
            Stamina = 0f;
        }
        SprintStaminaCheck();

        if (Stamina < StaminaMax)
        {
            Stamina += Time.deltaTime * PassiveStaminaGainRate;

        }
        else
        {
            Stamina = StaminaMax;
        }

        StaminaBar.value = Stamina;
        StaminaBar.maxValue = StaminaMax;
    }

    void SprintStaminaCheck()
    {
        if (IsSprinting)
        {
            Speed = Stamina > 0 ? SprintSpeed : NormalSpeed;
        }
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
