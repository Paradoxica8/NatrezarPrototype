using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 16f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.6f;

    private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool isInvulnerable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadMovementInput();
        HandleDashInput();

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = lastMoveDirection * dashSpeed;
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    private void ReadMovementInput()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            moveInput.y += 1;

        if (Keyboard.current.sKey.isPressed)
            moveInput.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x += 1;

        moveInput = moveInput.normalized;

        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }
    }

    private void HandleDashInput()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            TryDash();
        }
    }
    private Vector2 GetDashDirection()
    {
        if (moveInput != Vector2.zero)
        {
            return moveInput;
        }

        Vector3 mouseScreenPosition =
            Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition =
            Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 mouseDirection =
            mouseWorldPosition - transform.position;

        return mouseDirection.normalized;
    }

    private void TryDash()
    {
        if (isDashing)
            return;

        if (dashCooldownTimer > 0f)
            return;

        lastMoveDirection = GetDashDirection();

        if (lastMoveDirection == Vector2.zero)
            return;

        isDashing = true;
        isInvulnerable = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        Debug.Log("DASH!");
    }

    private void EndDash()
    {
        isDashing = false;
        isInvulnerable = false;
        rb.linearVelocity = Vector2.zero;
    } 
        public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}
