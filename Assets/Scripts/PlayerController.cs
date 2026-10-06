using System;
using System.Collections;
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
        Speed = NormalSpeed;
    }

    void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (RemainingJumps > 0)
        {
            if (Stamina > 25)
            {
                Stamina -= 25;
                rb.linearVelocityY = JumpVelocity;
                RemainingJumps--;
            }
            else
            {
                StartCoroutine(FlashStaminaBar());
            }
        }
    }
    IEnumerator FlashStaminaBar()
    {
        Image fill = StaminaBar.fillRect.GetComponent<Image>();
        Image background = StaminaBar.transform.Find("Background").GetComponent<Image>();

        Color originalFillColor = fill.color;

        Color originalBackgroundColor = background.color;

        fill.color = Color.red;

        background.color = Color.white;

        yield return new WaitForSeconds(0.15f);

        fill.color = originalFillColor;

        background.color = originalBackgroundColor;

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
        RemainingJumps = 1;
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
            if (Stamina == 0)
            {
                StartCoroutine(FlashStaminaBar());  // TODO: FIX
            }
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
