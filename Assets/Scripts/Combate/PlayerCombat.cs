/*using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;

    private float attackTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.right;

    private Animator animator;
    private PlayerInputHandler inputHandler; // Tu clase de input personalizada (InputAction, etc.)

    void Start()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (inputHandler.AttackPressed() && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }

        // Guarda la última dirección válida para la orientación del ataque
        Vector2 moveDir = inputHandler.GetMovementInput(); // Asume que devuelves Vector2 del Input
        if (moveDir != Vector2.zero)
            lastMoveDirection = moveDir.normalized;
    }

    void Attack()
    {
        // Reproducir animación si la hay
        animator?.SetTrigger("Attack");

        // Dirección en la que ataca
        Vector3 attackDir = lastMoveDirection;
        Vector3 attackPos = transform.position + attackDir * attackRange;

        // Detectar enemigos
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, 0.7f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage, transform);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Vector3 attackDir = lastMoveDirection;
        Vector3 attackPos = transform.position + attackDir * attackRange;
        Gizmos.DrawWireSphere(attackPos, 0.7f);
    }
}*/
