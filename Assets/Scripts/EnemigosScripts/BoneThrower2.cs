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
    private Animator animator;

    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool attacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Asegúrate de que el Animator esté en el mismo GameObject


        if (rb == null) Debug.LogError("Rigidbody2D no encontrado.");
        if (animator == null) Debug.LogError("Animator no encontrado.");
        if (firePoint == null) Debug.LogError("FirePoint no asignado.");
        if (bonePrefab == null) Debug.LogError("BonePrefab no asignado.");
    }

    void FixedUpdate()
    {
        cooldownTimer -= Time.fixedDeltaTime;

        if (attacking && targetPlayer != null)
        {
            Vector2 dir = ((Vector2)targetPlayer.position - rb.position).normalized;

            // Flip visual
            if (dir.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            if (cooldownTimer <= 0f)
            {
                animator.SetTrigger("Summon"); // Activar animación de invocación
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

        if (dir.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    public void Shoot()
    {
        if (targetPlayer == null) return;

        Vector2 directionToPlayer = (targetPlayer.position - firePoint.position).normalized;

        GameObject bone = Instantiate(bonePrefab, firePoint.position, Quaternion.identity);
        bone.GetComponent<BoneProjectile>()?.SetDirection(directionToPlayer);

        Debug.Log("¡Lanzando hueso hacia el jugador!");
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

    void StartAttack(Transform player)
    {
        animator.SetTrigger("Summon"); // Activar animación de invocación
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
    
    public void InvokeBone()
    {
        GameObject bone = Instantiate(bonePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Hueso invocado por evento");
    }
}
