using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_movement : MonoBehaviour
{
    //[SerializeField] private float speed = 3f;  // Velocidad de movimiento
    private Vector2 moveInput;  // Entrada del jugador
    private Animator playerAnimator;  // Referencia al Animator

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();  // Obtener el Animator del GameObject
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la entrada del jugador (horizontal y vertical)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Pasar las direcciones de movimiento al Animator
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);

        // Actualizar la animación de velocidad utilizando la magnitud del vector de movimiento
        moveInput = new Vector2(moveX, moveY).normalized;  // Normalizar para evitar que el movimiento diagonal sea más rápido
        playerAnimator.SetFloat("Speed", moveInput.magnitude);  // Usamos .magnitude para determinar si el jugador está moviéndose o no
    }
}

