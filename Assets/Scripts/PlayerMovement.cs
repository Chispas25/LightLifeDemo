using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Este método será llamado automáticamente si usas "Send Messages" en PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            LastMoveDirection = moveInput.normalized;
        }

        
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;

        
    }
}
