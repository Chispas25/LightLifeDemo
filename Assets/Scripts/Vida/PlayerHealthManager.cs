using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxLives = 3;
    public float reviveDuration = 6f;
    public float invulnerabilityDuration = 2f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    private float invulnerabilityTimer = 0f;
    public Light2D playerLight; // Asigna desde el Inspector si tienes una luz personalizada

    private int currentLives;
    private bool isDead = false;

    // Componentes a desactivar automáticamente
    private PlayerMovement movementScript;
    private PlayerInput inputComponent;

    void Awake()
    {
        currentLives = maxLives;
        movementScript = GetComponent<PlayerMovement>();
        inputComponent = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.Log("Vidas del jugador: " + GetCurrentLives());
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
                isInvulnerable = false;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || isInvulnerable) return;

        currentLives -= amount;

        if (currentLives <= 0)
            Die();
        else
            TriggerInvulnerability();
    }

    void TriggerInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashWhileInvulnerable());
    }   

    void Die()
    {
        isDead = true;

        if (playerLight != null)
            playerLight.enabled = false;

        if (movementScript != null)
            movementScript.enabled = false;

        if (inputComponent != null)
            inputComponent.enabled = false;
    }

    public void Revive()
    {
        if (!isDead) return;

        isDead = false;
        currentLives = 2;

        if (playerLight != null)
            playerLight.enabled = true;

        if (movementScript != null)
            movementScript.enabled = true;

        if (inputComponent != null)
            inputComponent.enabled = true;

        TriggerInvulnerability(); // ← aquí
    }

    IEnumerator FlashWhileInvulnerable()
    {
        float flashDelay = 0.2f;
        while (isInvulnerable)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(flashDelay);
        }

        if (spriteRenderer != null)
        spriteRenderer.enabled = true;
    }

    public int GetCurrentLives()
    {
    return currentLives;
    }

    public bool IsDead() => isDead;
}
