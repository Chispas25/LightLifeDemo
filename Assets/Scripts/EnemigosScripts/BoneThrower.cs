using UnityEngine;

public class BoneThrower : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float detectionDistance = 7f;
    public float attackCooldown = 2f;
    public GameObject bonePrefab;
    public Transform firePoint;
    public LayerMask playerLayer;

    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool attacking = false;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (attacking && targetPlayer != null)
        {
            // Mira hacia el jugador
            Vector2 dir = (targetPlayer.position - transform.position).normalized;
            transform.right = dir;

            if (cooldownTimer <= 0f)
            {
                Shoot();
                cooldownTimer = attackCooldown;
            }

            float lostRange = detectionDistance * 1.5f;
            if (Vector2.Distance(transform.position, targetPlayer.position) > lostRange)
            {
                StopAttack();
            }
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

        if (Vector2.Distance(transform.position, patrolTarget.position) < 0.2f)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
        transform.right = dir;
    }

    void Shoot()
    {
        GameObject bone = Instantiate(bonePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("¡Lanzando hueso!");
    }

    bool LookForPlayer(out Transform player)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionDistance, playerLayer);
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
