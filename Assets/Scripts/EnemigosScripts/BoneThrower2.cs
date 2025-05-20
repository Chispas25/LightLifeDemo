using UnityEngine;

public class BoneThrower2 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float detectionDistance = 7f;
    public float attackCooldown = 2f;
    public GameObject bonePrefab;
    public Transform firePoint;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool attacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        cooldownTimer -= Time.fixedDeltaTime;

        if (attacking && targetPlayer != null)
        {
            Vector2 dir = ((Vector2)targetPlayer.position - rb.position).normalized;

            // Visual Flip
            if (dir.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            if (cooldownTimer <= 0f)
            {
                Shoot();
                cooldownTimer = attackCooldown;
            }

            float lostRange = detectionDistance * 1.5f;
            if (Vector2.Distance(rb.position, targetPlayer.position) > lostRange)
                StopAttack();
        }
        else
        {
            Patrol();

            if (LookForPlayer(out Transform player))
                StartAttack(player);
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
        Vector2 dir = (target - rb.position).normalized;
        Vector2 newPos = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        // Visual Flip
        if (dir.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void Shoot()
    {
        GameObject bone = Instantiate(bonePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("¡Lanzando hueso!");
        // Puedes añadir física o lógica para el proyectil aquí.
    }

    bool LookForPlayer(out Transform player)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, transform.right, detectionDistance, playerLayer);
        if (hit.collider != null)
        {
            player = hit.collider.transform;
            return true;
        }

        player = null;
        return false;
    }

    void StartAttack(Transform player)
    {
        targetPlayer = player;
        attacking = true;
        Debug.Log("Jugador a la vista, lanzando huesos.");
    }

    void StopAttack()
    {
        targetPlayer = null;
        attacking = false;
        Debug.Log("Jugador fuera de rango, volviendo a patrullar.");
    }
}
