/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("ID de compatibilidad")]
    public string keyID;

    [Header("Estado y animación")]
    public Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        // Si no se asignó animador manualmente, intenta obtenerlo automáticamente
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Inicia en estado cerrado
        if (animator != null)
        {
            animator.ResetTrigger("Open");
            animator.Play("cerrarautopuerta", 0, 0); // ← Asegúrate de tener este clip en el animator
        }

        // Asegura que el collider esté activo (no trigger)
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = true;
            }
        }
    }

    // Método llamado desde la llave
    public bool TryOpen(string key)
    {
        if (isOpen) return false;

        if (key == keyID)
        {
            OpenDoor();
            return true;
        }

        return false;
    }

    // Lógica de apertura real
    private void OpenDoor()
    {
        isOpen = true;

        // Desactiva colisiones físicas (si no son trigger)
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }

        // Lanza animación si hay animador
        if (animator != null)
        {
            animator.SetTrigger("Open"); // ← Asegúrate de tener un parámetro "Open" en el Animator
        }
        else
        {
            Debug.Log("Puerta abierta (sin animación)");
            gameObject.SetActive(false); // Alternativa si no hay animación
        }

        Debug.Log($"¡Puerta {keyID} abierta!");
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("ID de compatibilidad")]
    public string keyID;

    [Header("Estado y animación")]
    public Animator animator;
    private bool isOpen = false;

    [Header("Audio")]
    public AudioClip openSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Si no se asignó animador manualmente, intenta obtenerlo automáticamente
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Agrega un AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Inicia en estado cerrado
        if (animator != null)
        {
            animator.ResetTrigger("Open");
            animator.Play("cerrarautopuerta", 0, 0); // ← Asegúrate de tener este clip en el animator
        }

        // Asegura que el collider esté activo (no trigger)
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = true;
            }
        }
    }

    // Método llamado desde la llave
    public bool TryOpen(string key)
    {
        if (isOpen) return false;

        if (key == keyID)
        {
            OpenDoor();
            return true;
        }

        return false;
    }

    // Lógica de apertura real
    private void OpenDoor()
    {
        isOpen = true;

        // Desactiva colisiones físicas (si no son trigger)
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }

        // Lanza animación si hay animador
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }

        // Reproducir sonido
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        Debug.Log($"¡Puerta {keyID} abierta!");
    }
}
