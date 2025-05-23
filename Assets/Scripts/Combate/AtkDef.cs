using UnityEngine;
using UnityEngine.InputSystem;

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
    public Vector2 LastMoveDirection { get; private set; } = Vector2.right;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
            LastMoveDirection = moveInput.normalized;
    }

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
        Vector2 attackOrigin = (Vector2)transform.position + attackDir * attackRange * 0.5f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackOrigin, attackRange * 0.5f, enemyLayer);

        foreach (Collider2D enemy in hits)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(attackDamage);

                // Opcional: aplicar retroceso
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDir * 100f); // Ajusta fuerza
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (movementScript == null) return;

        Vector2 dir = movementScript.LastMoveDirection;
        Vector2 pos = (Vector2)transform.position + dir * attackRange * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange * 0.5f);
    }
}
