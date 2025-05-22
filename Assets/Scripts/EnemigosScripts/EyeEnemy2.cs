using UnityEngine;

public class EyeEnemy2 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 1;
    public float knockbackForce = 1.5f;
    public float detectionDistance = 5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool chasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        cooldownTimer -= Time.fixedDeltaTime;

        if (chasing && targetPlayer != null)
        {
            float distance = Vector2.Distance(rb.position, targetPlayer.position);

            // Actualiza la dirección para la animación
            Vector2 dir = (targetPlayer.position - transform.position).normalized;
            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", dir.y);

            if (distance > attackRange)
                MoveTowards(targetPlayer.position);
            else
                TryAttack();

            if (distance > detectionDistance * 1.5f)
                StopChase();
        }
        else
        {
            Patrol();

            if (LookForPlayer(out Transform player))
                StartChase(player);
        }
    }

    void Patrol()
    {
        Transform patrolTarget = patrolPoints[currentPatrolIndex];
        MoveTowards(patrolTarget.position);

        if (Vector2.Distance(rb.position, patrolTarget.position) < 0.2f)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - rb.position).normalized;
        Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void TryAttack()
    {
        if (cooldownTimer <= 0f)
        {
            // Calcular dirección hacia el jugador
            Vector2 dir = ((Vector2)targetPlayer.position - rb.position).normalized;

            // Enviar la dirección al Animator
            animator.SetFloat("Dirx", dir.x);
            animator.SetFloat("Diry", dir.y);
            // Activar animación de ataque
            animator.SetTrigger("Attack");

            // Aplicar retroceso al jugador
            Rigidbody2D playerRb = targetPlayer.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (playerRb.position - rb.position).normalized;
                playerRb.velocity = knockbackDir * 10f; // Ajusta fuerza a tu gusto
            }

            // Aplicar daño si tienes un script de salud
            // targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damage);

            cooldownTimer = attackCooldown;
            Debug.Log("El ojo ataca al jugador.");
        }
    }

    bool LookForPlayer(out Transform detectedPlayer)
    {
        Collider2D hit = Physics2D.OverlapCircle(rb.position, detectionDistance, playerLayer);
        if (hit != null)
        {
            detectedPlayer = hit.transform;
            return true;
        }

        detectedPlayer = null;
        return false;
    }

    void StartChase(Transform player)
    {
        targetPlayer = player;
        chasing = true;
        Debug.Log("¡Jugador detectado! Persiguiendo...");
    }

    void StopChase()
    {
        targetPlayer = null;
        chasing = false;
        Debug.Log("Jugador perdido. Volviendo a patrullar.");
    }
}
