using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerMotor : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed = 6f;

    [Header("Jump/Gravity")]
    [SerializeField] float gravity = -25f;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float groundedSnap = -2f;

    Vector2 moveInput;
    bool jumpQueued;
    float verticalVelocity;

    void Reset()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) jumpQueued = true;
    }

    void Update()
    {
        bool grounded = controller.isGrounded;

        if (grounded && verticalVelocity < 0f)
            verticalVelocity = groundedSnap;

        if (jumpQueued && grounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        jumpQueued = false;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        Vector3 velocity =
            moveSpeed * move +
            Vector3.up * verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}