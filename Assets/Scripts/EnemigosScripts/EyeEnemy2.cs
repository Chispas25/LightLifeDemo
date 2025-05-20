using UnityEngine;

public class EyeEnemy2 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 1;
    public float detectionDistance = 5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool chasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        cooldownTimer -= Time.fixedDeltaTime;

        if (chasing && targetPlayer != null)
        {
            float distance = Vector2.Distance(rb.position, targetPlayer.position);

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

        // Flip visual según dirección horizontal
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
            Debug.Log("El ojo golpea al jugador.");
            // targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            cooldownTimer = attackCooldown;
        }
    }

    bool LookForPlayer(out Transform detectedPlayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, transform.right, detectionDistance, playerLayer);
        if (hit.collider != null)
        {
            detectedPlayer = hit.collider.transform;
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
