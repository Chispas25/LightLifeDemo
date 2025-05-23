using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 1;
    public float detectionDistance = 5f;
    public LayerMask playerLayer;

    private float cooldownTimer = 0f;
    private int currentPatrolIndex = 0;
    private Transform targetPlayer;
    private bool chasing = false;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (chasing && targetPlayer != null)
        {
            float distance = Vector2.Distance(transform.position, targetPlayer.position);

            if (distance > attackRange)
                MoveTowards(targetPlayer.position);
            else
                TryAttack();

            // Perder al jugador si se aleja demasiado
            if (distance > detectionDistance * 1.5f)
                StopChase();
        }
        else
        {
            Patrol();

            // Revisa si hay jugador al frente
            if (LookForPlayer(out Transform player))
                StartChase(player);
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
        transform.right = dir; // Hace que el enemigo mire hacia donde se mueve
    }

    void TryAttack()
    {
        if (cooldownTimer <= 0f)
        {
            Debug.Log("El ojo golpea al jugador.");
            //targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            //Hay que hacer la barra de vida!!!
            cooldownTimer = attackCooldown;
        }
    }

    bool LookForPlayer(out Transform detectedPlayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionDistance, playerLayer);
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
