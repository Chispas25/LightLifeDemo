using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AtkDef : MonoBehaviour
{
    private Animator attackAnimator;
    public int framesToWait = 30;
    private int currentFrame = 0;
    private bool isAttacking = false;

    [Header("Parámetros de ataque")]
    public float attackRange = 1.5f;
    public float attackDamage = 1f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private PlayerMovement movementScript;

    private Vector2 moveInput = Vector2.zero;
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    

    private void Start()
    {
        attackAnimator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            currentFrame++;

            if (currentFrame == 5) // Ajusta el frame en el que el golpe ocurre
            {
                PerformAttack();
            }

            if (currentFrame > framesToWait)
            {
                currentFrame = 0;
                attackAnimator.SetBool("Attack", false);
                isAttacking = false;
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isAttacking)
        {
            attackAnimator.SetBool("Attack", true);
            isAttacking = true;
            currentFrame = 0;
        }
    }

    private void PerformAttack()
    {
        Vector2 attackDir = movementScript.LastMoveDirection;
        Vector2 attackOrigin = (Vector2)transform.position + attackDir * attackRange;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackOrigin, attackRange, enemyLayer);

        HashSet<EnemyHealth> damaged = new HashSet<EnemyHealth>();

        foreach (Collider2D enemy in hits)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var enemyHealth) && !damaged.Contains(enemyHealth))
            {
                // Ángulo entre dirección del ataque y dirección hacia el enemigo
                Vector2 dirToEnemy = (enemy.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(attackDir, dirToEnemy);

                if (angle < 90f) // solo enemigos en el "arco frontal"
                {
                    damaged.Add(enemyHealth);
                    Debug.Log($"Golpeando a {enemy.name}");

                    enemyHealth.TakeDamage(attackDamage);

                    // Retroceso opcional
                    Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                        rb.AddForce(knockbackDir);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 dir;

        // Usa la dirección del movimiento si está disponible en juego
        if (Application.isPlaying && movementScript != null)
        {
            dir = movementScript.LastMoveDirection;
        }
        else
        {
            dir = Vector2.down; // Dirección por defecto en editor
        }

        float angleSpan = 180f;
        int segments = 20;
        float radius = attackRange;

        Vector2 origin = (Vector2)transform.position; // 👈 El arco parte del centro

        Gizmos.color = Color.red;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -angleSpan / 2f + (angleSpan / segments) * i;
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angle) * dir.normalized;
            Vector2 endPoint = origin + rotatedDir * radius;
            Gizmos.DrawLine(origin, endPoint);
        }

        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawWireSphere(origin, radius);
    }
}
