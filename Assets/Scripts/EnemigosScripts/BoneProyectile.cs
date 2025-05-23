using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 3f;
    public int damage = 1;

    private float timer = 0f;
    private Vector2 moveDirection = Vector2.zero;

    public LayerMask playerLayer;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // No rotamos el proyectil porque la animación ya lo representa visualmente
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Player")) != 0)
        {
            // Aplicar daño si tiene el PlayerHealthManager
            PlayerHealthManager health = other.GetComponent<PlayerHealthManager>();
            if (health != null)
            {
                health.TakeDamage(damage);

                // Aplicar retroceso físico
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDir = (rb.position - (Vector2)transform.position).normalized;
                    float knockbackForce = 10f; // puedes ajustar esta fuerza
                    rb.velocity = knockbackDir * knockbackForce;
                }
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
