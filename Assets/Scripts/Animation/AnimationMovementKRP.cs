using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AnimationMovementKRP : MonoBehaviour
{
    private Vector2 moveInput;
    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Este método será llamado automáticamente por el sistema de Input cuando se detecte movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Actualizar animaciones
        playerAnimator.SetFloat("Horizontal", moveInput.x);
        playerAnimator.SetFloat("Vertical", moveInput.y);
        playerAnimator.SetFloat("Speed", moveInput.magnitude);
    }
}
